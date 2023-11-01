using AutoMapper;
using FogoProject.Business.Categories.DTOs;
using FogoProject.Business.Products.DTOs;
using FogoProject.DataAccess.Abstract;
using FogoProject.DataAccess.Concrete;
using FogoProject.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task Add(CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
                throw new ArgumentNullException("createProductDTO is null.");

            if (_categoryRepository.GetById(createProductDTO.CategoryID) == null)
                throw new ArgumentNullException("Category does not exist.");

            var product = _mapper.Map<Product>(createProductDTO);

            if (product == null)
                throw new ArgumentNullException("Product mapping failed.");

            await _productRepository.Add(product);

        }

        public async Task Delete(int productId)
        {
            if (await _productRepository.GetById(productId) == null)
                throw new ArgumentNullException("Product does not exist.");

            await _productRepository.Delete(productId);
        }

        public IQueryable<GetProductDTO> GetAll()
        {
            return _mapper.ProjectTo<GetProductDTO>(_productRepository.GetAll());
        }

        public async Task<GetProductDTO> GetById(int productId)
        {
            return _mapper.Map<GetProductDTO>(await _productRepository.GetById(productId));
        }

        public async Task Update(int productId, UpdateProductDTO updateProductDTO)
        {
            var updateProduct = await _productRepository.GetById(productId);

            if (updateProduct == null)
                throw new ArgumentNullException("Product does not exist.");

            if (updateProductDTO == null)
                throw new ArgumentNullException("updateProductDTO is null.");

            if(_categoryRepository.GetById(updateProductDTO.CategoryID) == null)
                throw new ArgumentNullException("Category does not exist.");



            updateProduct.Name = updateProductDTO.Name;
            updateProduct.Description = updateProductDTO.Description;
            updateProduct.Price = updateProductDTO.Price;
            updateProduct.CategoryID = updateProductDTO.CategoryID;

            await _productRepository.Update(updateProduct);
        }
    }
}
