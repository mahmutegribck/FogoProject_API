using FogoProject.DataAccess.Abstract;
using FogoProject.DataAccess.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FogoProjectDBContext _dbContext;
        private ProductRepository _productRepository;
        private CategoryRepository _categoryRepository;

        public UnitOfWork(FogoProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository = _categoryRepository ?? new CategoryRepository(_dbContext);

        public IProductRepository ProductRepository => _productRepository = _productRepository ?? new ProductRepository(_dbContext);

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
