using System.ComponentModel.DataAnnotations;

namespace WeddingPlatform.ViewModels
{
    public class TemplateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string HTML { get; set; }

        [Required]
        public string CSS { get; set; }

        public bool IsActive { get; set; }
    }
}