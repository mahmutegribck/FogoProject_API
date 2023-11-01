using AutoMapper;
using FogoProject.Business.Categories.DTOs;
using FogoProject.Business.Products.DTOs;
using FogoProject.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryWithProductsDto>().ReverseMap();
            
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, GetProductDTO>().ReverseMap();
            


        }
    }
}
