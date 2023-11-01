using FogoProject.DataAccess.Abstract;
using FogoProject.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.DataAccess.Concrete
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(FogoProjectDBContext dBContext) : base(dBContext)
        {
        }
    }
}
