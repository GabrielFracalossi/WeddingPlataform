public class GiftRegistry
{
    public int Id { get; set; }
    public int WeddingSiteId { get; set; }
    public virtual WeddingSite WeddingSite { get; set; }
    public string ItemName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPurchased { get; set; }
    public string PurchasedBy { get; set; }
}