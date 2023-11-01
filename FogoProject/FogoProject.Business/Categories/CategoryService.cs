using AutoMapper;
using FogoProject.Business.Categories.DTOs;
using FogoProject.Business.Products.DTOs;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task Add(CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null)
                throw new ArgumentNullException("createCategoryDTO is null.");

            if (!await _categoryRepository.IsCategoryNameUnique(null,createCategoryDTO.Name))
                throw new InvalidOperationException("A category with the same name already exists.");

            var category = _mapper.Map<Category>(createCategoryDTO);

            if (category == null)
                throw new ArgumentNullException("Category mapping failed.");

            await _categoryRepository.Add(category);
        }

        public async Task Delete(int categoryId)
        {
            if (await _categoryRepository.GetById(categoryId) == null)
                throw new ArgumentNullException("Category does not exist.");

            await _categoryRepository.Delete(categoryId);
        }

        public IQueryable<GetCategoryDTO> GetAll()
        {
            return _mapper.ProjectTo<GetCategoryDTO>(_categoryRepository.GetAll());
        }

        public async Task<GetCategoryDTO> GetById(int categoryId)
        {
            return _mapper.Map<GetCategoryDTO>(await _categoryRepository.GetById(categoryId));
        }

        public async Task<GetCategoryWithProductsDto> GetCategoryWithProducts(int categoryId)
        {
            return _mapper.Map<GetCategoryWithProductsDto>(await _categoryRepository.GetCategoryWithProducts(categoryId));
        }

        public async Task Update(int categoryId, UpdateCategoryDTO updateCategoryDTO)
        {
            var updateCategory = await _categoryRepository.GetById(categoryId);

            if (updateCategory == null)
                throw new ArgumentNullException("Category does not exist.");

            if (updateCategoryDTO == null)
                throw new ArgumentNullException("updateCategoryDTO is null.");

            if (!await _categoryRepository.IsCategoryNameUnique(categoryId, updateCategoryDTO.Name))
                throw new InvalidOperationException("A category with the same name already exists.");


            updateCategory.Name = updateCategoryDTO.Name;
            updateCategory.Description = updateCategoryDTO.Description;
            updateCategory.Description = updateCategoryDTO.Description;

            await _categoryRepository.Update(updateCategory);
        }
    }
}
