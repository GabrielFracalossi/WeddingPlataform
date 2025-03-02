using System.ComponentModel.DataAnnotations;
using WeddingPlatform.Models;

namespace WeddingPlatform.ViewModels
{
    public class CreateWeddingSiteViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Wedding Date")]
        public DateTime WeddingDate { get; set; }

        [Required]
        [Display(Name = "Template")]
        public int TemplateId { get; set; }

        public IEnumerable<Template> Templates { get; set; }
    }
}