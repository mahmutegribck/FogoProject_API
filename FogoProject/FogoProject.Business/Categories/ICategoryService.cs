using FogoProject.Business.Categories.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Categories
{
    public interface ICategoryService
    {
        Task<GetCategoryDTO> GetById(int categoryId);
        IQueryable<GetCategoryDTO> GetAll();
        Task Add(CreateCategoryDTO createCategoryDTO);
        Task Update(int categoryId,UpdateCategoryDTO updateCategoryDTO);
        Task Delete(int categoryId);
        Task<GetCategoryWithProductsDto> GetCategoryWithProducts(int categoryId);
    }
}
    