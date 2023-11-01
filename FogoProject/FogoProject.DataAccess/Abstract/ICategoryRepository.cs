using FogoProject.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.DataAccess.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetCategoryWithProducts(int categoryId);
        Task<bool> IsCategoryNameUnique(int? categoryId, string categoryName);
        
    }
}
