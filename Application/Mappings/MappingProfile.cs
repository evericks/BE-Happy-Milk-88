using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Domain.Models.Authentications;
using Domain.Models.Creates;
using Domain.Models.Updates;
using Domain.Models.Views;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Data type
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<double?, double>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<Guid?, Guid>().ConvertUsing((src, dest) => src ?? dest);
            CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

            // Auth
            CreateMap<Customer, AuthModel>();

            // Customer
            CreateMap<Customer, CustomerViewModel>();

            // Cart
            CreateMap<Cart, CartViewModel>();

            // Cart Item
            CreateMap<CartItem, CartItemViewModel>();
            CreateMap<CartItemCreateModel, CartItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            // Category
            CreateMap<Category, CategoryViewModel>();

            // Product
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Sold, opt => opt.MapFrom(src => src.OrderDetails.Sum(x => x.Quantity)));
            CreateMap<ProductCreateModel, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ProductStatuses.ACTIVE))
                .ForMember(dest => dest.ProductCategories, opt => opt.MapFrom((src, dest) => src.ProductCategories.Select(x =>
                new ProductCategory
                {
                    Id = Guid.NewGuid(),
                    ProductId = dest.Id,
                    CategoryId = x.CategoryId,
                })));
            CreateMap<ProductUpdateModel, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Product Category
            CreateMap<ProductCategory, ProductCategoryViewModel>();
        }
    }
}
