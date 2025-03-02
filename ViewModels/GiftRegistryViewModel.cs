using System.ComponentModel.DataAnnotations;

namespace WeddingPlatform.ViewModels
{
    public class GiftRegistryViewModel
    {
        public int WeddingSiteId { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }
    }
}