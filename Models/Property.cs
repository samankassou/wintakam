namespace Wintakam.Models;

public class Property
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "XAF";
    public string Location { get; set; } = string.Empty;
    public string? Address { get; set; }
    public decimal? Area { get; set; }
    public int? Bedrooms { get; set; }
    public int? Bathrooms { get; set; }
    public string? ImageUrl { get; set; }
    public List<string>? ImageUrls { get; set; }
    public PropertyStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public List<string>? Features { get; set; }
}
