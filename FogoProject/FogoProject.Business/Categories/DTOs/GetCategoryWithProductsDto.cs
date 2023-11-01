using FogoProject.Business.Products.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Categories.DTOs
{
    public class GetCategoryWithProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<GetProductDTO> Products { get; set; }
    }
}
