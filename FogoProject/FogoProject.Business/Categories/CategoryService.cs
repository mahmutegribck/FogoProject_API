using AutoMapper;
using FogoProject.Business.Categories.DTOs;
using FogoProject.Business.Products.DTOs;
using FogoProject.DataAccess;
using FogoProject.DataAccess.Abstract;
using FogoProject.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Add(CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null)
                throw new ArgumentException("createCategoryDTO is null.");

            if (!await _unitOfWork.CategoryRepository.IsCategoryNameUnique(null, createCategoryDTO.Name))
                throw new InvalidOperationException("A category with the same name already exists.");

            var category = _mapper.Map<Category>(createCategoryDTO);

            if (category == null)
                throw new ArgumentException("Category mapping failed.");

            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(int categoryId)
        {
            if (await _unitOfWork.CategoryRepository.GetById(categoryId) == null)
                throw new ArgumentException("Category does not exist.");

            await _unitOfWork.CategoryRepository.Delete(categoryId);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<GetCategoryDTO>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll().ToListAsync();
            var categoriesList = _mapper.Map<IEnumerable<Category>, IEnumerable<GetCategoryDTO>>(categories);
            return categoriesList;
        }

        public async Task<GetCategoryDTO> GetById(int categoryId)
        {
            
            return _mapper.Map<GetCategoryDTO>(await _unitOfWork.CategoryRepository.GetById(categoryId));
        }

        public async Task<GetCategoryWithProductsDto> GetCategoryWithProducts(int categoryId)
        {
            return _mapper.Map<GetCategoryWithProductsDto>(await _unitOfWork.CategoryRepository.GetCategoryWithProducts(categoryId));
        }

        public async Task Update(int categoryId, UpdateCategoryDTO updateCategoryDTO)
        {
            var updateCategory = await _unitOfWork.CategoryRepository.GetById(categoryId);

            if (updateCategory == null)
                throw new ArgumentException("Category does not exist.");

            if (updateCategoryDTO == null)
                throw new ArgumentException("updateCategoryDTO is null.");

            if (!await _unitOfWork.CategoryRepository.IsCategoryNameUnique(categoryId, updateCategoryDTO.Name))
                throw new InvalidOperationException("A category with the same name already exists.");


            updateCategory.Name = updateCategoryDTO.Name;
            updateCategory.Description = updateCategoryDTO.Description;
            updateCategory.Description = updateCategoryDTO.Description;

            await _unitOfWork.CategoryRepository.Update(updateCategory);
            await _unitOfWork.CommitAsync();
        }
    }
}
