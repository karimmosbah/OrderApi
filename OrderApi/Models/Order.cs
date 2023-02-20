using System.ComponentModel.DataAnnotations;

namespace OrderApi.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name Field is Required")]
        [StringLength(maximumLength:100, MinimumLength =2)]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Price Field is Required")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Quantity Field is Required")]
        public int Quantity { get; set; }
        public string Image { get; set; }
    }
}
