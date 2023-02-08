using System.ComponentModel.DataAnnotations;

namespace ProjectASP.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Username không được để trống")]
        [Display(Name = "Tài khoản")]
        public string username { get; set; }

        [Required(ErrorMessage = "Mật Khẩu không được để trống")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật Khẩu")]
        public string password { get; set; }
    }
}
