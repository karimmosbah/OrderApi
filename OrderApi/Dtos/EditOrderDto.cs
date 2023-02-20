using System.ComponentModel.DataAnnotations;

namespace OrderApi.Dtos
{
    public class EditOrderDto
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, MinimumLength = 2)]
        public string? Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile? Image { get; set; }
        public string ImagePath { get; set; }

    }

}
