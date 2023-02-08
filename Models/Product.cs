using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectASP.Models
{
    public class Product
    {
        [Key]
        public int id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm")] 
        public string name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số lượng sản phẩm")] 
        public int quantity { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập giá sản phẩm")] 
        public double price { get; set; }
         
        public string? img { get; set; }
        
        [ForeignKey("Category")]
        public int? categoryId { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mô tả sản phẩm")] 
        public string? description { get; set; }
    }
}
