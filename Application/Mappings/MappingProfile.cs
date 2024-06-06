using AutoMapper;
using Domain.Entities;
using Domain.Models.Views;

namespace Application.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryViewModel>();
        }
    }
}
