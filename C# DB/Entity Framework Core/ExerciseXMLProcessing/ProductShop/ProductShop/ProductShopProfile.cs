using AutoMapper;
using ProductShop.DTOs.Export;
using ProductShop.DTOs.Import;
using ProductShop.Models;

namespace ProductShop
{
    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            CreateMap<ImportUsersDTO, User>();
            CreateMap<ImportProductsDTO, Product>();
            CreateMap<ImportCategoriesDTO, Category>();
            CreateMap<ImportCategoryProductsDTO, CategoryProduct>();
            CreateMap<Product, ExportProductsDTO>();
            CreateMap<User, ExportSoldProductsDTO>();
            
        }
    }
}
