using FogoProject.DataAccess.Abstract;
using FogoProject.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.DataAccess.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(FogoProjectDBContext dBContext) : base(dBContext)
        {
        }



        public async Task<Category> GetCategoryWithProducts(int categoryId)
        {

            var category = await GetAll().Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == categoryId);
            return category;
        }


        public async Task<bool> IsCategoryNameUnique(int? categoryId, string categoryName)
        {
            if (categoryId.HasValue)
            {
                return await GetAll().AnyAsync(c => c.Name == categoryName && c.Id != categoryId) == false;
            }
            else
            {
                return await GetAll().AnyAsync(c => c.Name == categoryName) == false;
            }
        }
    }
}
