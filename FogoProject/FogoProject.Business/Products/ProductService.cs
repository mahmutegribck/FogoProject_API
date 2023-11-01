using AutoMapper;
using FogoProject.Business.Categories.DTOs;
using FogoProject.Business.Products.DTOs;
using FogoProject.DataAccess;
using FogoProject.DataAccess.Abstract;
using FogoProject.DataAccess.Concrete;
using FogoProject.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Products
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Add(CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
                throw new ArgumentException("createProductDTO is null.");
            var pCategory = await _unitOfWork.CategoryRepository.GetById(createProductDTO.CategoryID);
            if (pCategory == null)
                throw new ArgumentException("Category does not exist.");

            var product = _mapper.Map<Product>(createProductDTO);

            if (product == null)
                throw new ArgumentException("Product mapping failed.");

            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int productId)
        {
            if (await _unitOfWork.ProductRepository.GetById(productId) == null)
                throw new ArgumentException("Product does not exist.");

            await _unitOfWork.ProductRepository.Delete(productId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<GetProductDTO>> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.GetAll().ToListAsync();
            var productsList = _mapper.Map<IEnumerable<Product>, IEnumerable<GetProductDTO>>(products);
            return productsList;
            
        }

        public async Task<GetProductDTO> GetById(int productId)
        {
            return _mapper.Map<GetProductDTO>(await _unitOfWork.ProductRepository.GetById(productId));
        }

        public async Task Update(int productId, UpdateProductDTO updateProductDTO)
        {
            var updateProduct = await _unitOfWork.ProductRepository.GetById(productId);

            if (updateProduct == null)
                throw new ArgumentException("Product does not exist.");

            if (updateProductDTO == null)
                throw new ArgumentException("updateProductDTO is null.");
            var upProductCategory = await _unitOfWork.CategoryRepository.GetById(updateProductDTO.CategoryID);
            if (upProductCategory == null)
                throw new ArgumentException("Category does not exist.");



            updateProduct.Name = updateProductDTO.Name;
            updateProduct.Description = updateProductDTO.Description;
            updateProduct.Price = updateProductDTO.Price;
            updateProduct.CategoryID = updateProductDTO.CategoryID;

            await _unitOfWork.ProductRepository.Update(updateProduct);
            await _unitOfWork.CommitAsync();
        }
    }
}
