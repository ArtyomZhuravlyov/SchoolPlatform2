using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using SchoolPlatform2.Business;
using SchoolPlatform2.DataAccess;
using SchoolPlatform2.Server.Configs;
using SchoolPlatform2.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var authSection = builder.Configuration.GetSection(AuthConfig.Position);
var authConfig = authSection.Get<AuthConfig>();


builder.Services.Configure<AuthConfig>(authSection);


string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("SchoolPlatform2.Server")));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<AuthService>();

// options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true убирает [Required] для передачи в контроллер с модели по умолчанию
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    // Помогает избежать зацикленности в серилизации
     .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddRazorPages();

var app = builder.Build();


// Заменяет Update DataBase
using (var serviceScope = ((IApplicationBuilder)app).ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
{
    if (serviceScope != null)
    {
        var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
