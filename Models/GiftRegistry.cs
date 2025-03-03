using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlatform.Models
{
    public class GiftRegistry
    {
        public int Id { get; set; }
        public int WeddingSiteId { get; set; }
        public virtual WeddingSite? WeddingSite { get; set; }
        
        [Required]
        public string ItemName { get; set; } = string.Empty;
        
        [Required]
        public string Description { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        public string? ImageUrl { get; set; }
        public bool IsPurchased { get; set; }
        public string? PurchasedBy { get; set; }
    }
}