using FogoProject.Business.Categories;
using FogoProject.Business.Categories.DTOs;
using FogoProject.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace FogoProject.API.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAll().ToListAsync();

            if (categories.Count == 0) return NotFound();

            return Ok(categories);
        }


        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int categoryId)
        {
            var category = await _categoryService.GetById(categoryId);

            if(category == null) return NotFound();

            return Ok(category);
        }


        [HttpGet("with-products/{categoryId}")]
        public async Task<IActionResult> GetCategoryWithProducts([FromRoute] int categoryId)
        {
            var categoryWithProducts = await _categoryService.GetCategoryWithProducts(categoryId);

            if (categoryWithProducts == null) return NotFound();

            return Ok(categoryWithProducts);
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _categoryService.Add(createCategoryDTO);

            return Ok();

        }


        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int categoryId, [FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _categoryService.Update(categoryId, updateCategoryDTO);

            return NoContent();
        }


        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            await _categoryService.Delete(categoryId);

            return NoContent();
        }
    }
}
