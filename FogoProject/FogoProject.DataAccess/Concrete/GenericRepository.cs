using FogoProject.DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.DataAccess.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly FogoProjectDBContext _dBContext;

        public GenericRepository(FogoProjectDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task Add(TEntity entity)
        {
            await _dBContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await _dBContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _dBContext.Set<TEntity>().Remove(entity);
                
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dBContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _dBContext.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            _dBContext.Set<TEntity>().Update(entity);
           
        }
    }
}
