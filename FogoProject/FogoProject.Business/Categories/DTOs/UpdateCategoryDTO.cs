using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogoProject.Business.Categories.DTOs
{
    public class UpdateCategoryDTO
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tanım zorunludur.")]
        public string Description { get; set; }

    }
}
