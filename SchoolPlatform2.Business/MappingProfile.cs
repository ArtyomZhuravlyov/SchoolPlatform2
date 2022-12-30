using AutoMapper;
using SchoolPlatform2.DataAccess;
using SchoolPlatform2.DataAccess.UserDA;
using SchoolPlatform2.Shared.DTO;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolPlatform2.Business
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Lesson, Lesson>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Course, Course>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Group, Group>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<User, User>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Student, Student>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<LessonDTO, Lesson>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<CourseDTO, Course>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ModuleHomeWorkDTO, ModuleHomeWork>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<HomeWorkDTO, HomeWork>().ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<Lesson, LessonDTO>();
            CreateMap<Course, CourseDTO>();
            CreateMap<ModuleHomeWork, ModuleHomeWorkDTO>();
            CreateMap<HomeWork, HomeWorkDTO>();

            //CreateMap<Product, ProductDTO>().ReverseMap();
            //CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            //CreateMap<OrderHeaderDTO, OrderHeader>().ReverseMap();
            //CreateMap<OrderDetail, OrderDetailDTO>().ReverseMap();
            //CreateMap<OrderDTO, Order>().ReverseMap();
            ////CreateMap<CategoryDTO, Category>();
        }
    }
}