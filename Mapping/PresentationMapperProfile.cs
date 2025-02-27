using Assignment1_MVC.ViewModels;
using AutoMapper;
using FUNewsManagementSystem.BLL.DTOs;

namespace Assignment1_MVC.Mapping
{
    public class PresentationMapperProfile : Profile
    {
        public PresentationMapperProfile()
        {
            CreateMap<NewsArticleDTO, NewsArticleViewModel>().ReverseMap();
            CreateMap<CategoryDTO, CategoryViewModel>().ReverseMap();
            CreateMap<SystemAccountDTO, SystemAccountViewModel>().ReverseMap();
        }
    }
}
