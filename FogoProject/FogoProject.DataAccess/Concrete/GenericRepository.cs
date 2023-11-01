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
            await _dBContext.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            //using var context = new FogoProjectDBContext();
            var entity = await _dBContext.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                _dBContext.Set<TEntity>().Remove(entity);
                await _dBContext.SaveChangesAsync();
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            //using var context = new FogoProjectDBContext();
            return _dBContext.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity> GetById(int id)
        {
            //using var context = new FogoProjectDBContext();
            return await _dBContext.Set<TEntity>().FindAsync(id);
        }

        public async Task Update(TEntity entity)
        {
            //using var context = new FogoProjectDBContext();
            _dBContext.Set<TEntity>().Update(entity);
            await _dBContext.SaveChangesAsync();
        }
    }
}
