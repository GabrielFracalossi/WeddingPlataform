public class Template
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string HTML { get; set; }
    public string CSS { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<WeddingSite> WeddingSites { get; set; }
}