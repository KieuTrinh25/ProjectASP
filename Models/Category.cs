using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectASP.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required(ErrorMessage = "Yêu cầu nhập tên danh mục sản phẩm")]
        public string name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mô tả danh mục sản phẩm")]
        public string description { get; set; }
    }
}
