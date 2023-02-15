using System.ComponentModel.DataAnnotations;

namespace ProjectASP.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
         
        public string code { get; set; }
         
        public string status { get; set; }
    }
}
