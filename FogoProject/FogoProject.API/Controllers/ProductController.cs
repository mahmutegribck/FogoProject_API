using FogoProject.Business.Categories.DTOs;
using FogoProject.Business.Products;
using FogoProject.Business.Products.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FogoProject.API.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAll();

            if (!products.Any()) return NotFound();

            return Ok(products);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProduct([FromRoute] int productId)
        {
            var product = await _productService.GetById(productId);

            if (product == null) return NotFound();

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _productService.Add(createProductDTO);

            return Ok();

        }


        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int productId, [FromBody] UpdateProductDTO updateProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            await _productService.Update(productId, updateProductDTO);

            return NoContent();
        }


        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int productId)
        {
            await _productService.Delete(productId);

            return NoContent();
        }
    }
}
