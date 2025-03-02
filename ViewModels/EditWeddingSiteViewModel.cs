using System.ComponentModel.DataAnnotations;
using WeddingPlatform.Models;

namespace WeddingPlatform.ViewModels
{
    public class EditWeddingSiteViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Wedding Date")]
        public DateTime WeddingDate { get; set; }

        [Display(Name = "Custom CSS")]
        public string CustomCSS { get; set; }

        [Display(Name = "Custom HTML")]
        public string CustomHTML { get; set; }

        [Required]
        [Display(Name = "Template")]
        public int TemplateId { get; set; }

        public IEnumerable<Template> Templates { get; set; }
    }
}