using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Products.DTOs
{
    public class CreateProductDTO
    {
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tanım zorunludur.")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Geçerli bir fiyat girin.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public int CategoryID { get; set; }
    }
}
