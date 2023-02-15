using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProjectASP.Models
{
    public class OrderDetail
    {
        [Key]
        public int id { get; set; }

        public int orderId { get; set; }
        [ValidateNever]
        public Order order { get; set; }
        public int productId { get; set; }
        [ValidateNever]
        public Product product { get; set; }

        public int quantity { get; set; }
    }
}
