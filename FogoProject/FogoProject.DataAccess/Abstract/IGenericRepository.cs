using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.DataAccess.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetById(int id);
        IQueryable<TEntity> GetAll();
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
    }
}
