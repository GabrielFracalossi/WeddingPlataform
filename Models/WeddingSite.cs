namespace WeddingPlatform.Models
{
    public class WeddingSite
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public int? TemplateId { get; set; }
        public virtual Template Template { get; set; }
        public string CustomCSS { get; set; }
        public string CustomHTML { get; set; }
        public DateTime WeddingDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<GiftRegistry> GiftRegistry { get; set; }
    }
}