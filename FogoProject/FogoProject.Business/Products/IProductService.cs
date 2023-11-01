using FogoProject.Business.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Products
{
    public interface IProductService
    {
        Task<GetProductDTO> GetById(int productId);
        Task<IEnumerable<GetProductDTO>> GetAll();
        Task Add(CreateProductDTO createProductDTO);
        Task Update(int productId, UpdateProductDTO updateProductDTO);
        Task Delete(int productId);
    }
}
