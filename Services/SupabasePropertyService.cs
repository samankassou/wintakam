using Supabase;
using Wintakam.Models;
using Postgrest.Attributes;
using Postgrest.Models;

namespace Wintakam.Services;

public class SupabasePropertyService : IPropertyService
{
    private readonly Supabase.Client _supabaseClient;

    public SupabasePropertyService(Supabase.Client supabaseClient)
    {
        _supabaseClient = supabaseClient;
    }

    public async Task<List<Property>> GetAllPropertiesAsync()
    {
        try
        {
            var response = await _supabaseClient
                .From<SupabaseProperty>()
                .Get();

            if (response?.Models == null)
                return new List<Property>();

            return response.Models.Select(MapToProperty).ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching properties: {ex.Message}");
            throw new Exception(TranslateError(ex.Message));
        }
    }

    public async Task<Property?> GetPropertyByIdAsync(string id)
    {
        try
        {
            var response = await _supabaseClient
                .From<SupabaseProperty>()
                .Where(p => p.Id == id)
                .Single();

            return response != null ? MapToProperty(response) : null;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching property by id: {ex.Message}");
            throw new Exception(TranslateError(ex.Message));
        }
    }

    public async Task<List<Property>> GetPropertiesByOwnerAsync(string ownerId)
    {
        try
        {
            var response = await _supabaseClient
                .From<SupabaseProperty>()
                .Where(p => p.OwnerId == ownerId)
                .Get();

            if (response?.Models == null)
                return new List<Property>();

            return response.Models.Select(MapToProperty).ToList();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching properties by owner: {ex.Message}");
            throw new Exception(TranslateError(ex.Message));
        }
    }

    private Property MapToProperty(SupabaseProperty supabaseProperty)
    {
        // Parse property type enum
        PropertyType propertyType = PropertyType.Autre;
        if (!string.IsNullOrEmpty(supabaseProperty.PropertyType))
        {
            Enum.TryParse(supabaseProperty.PropertyType, true, out propertyType);
        }

        // Parse status enum
        PropertyStatus status = PropertyStatus.Disponible;
        if (!string.IsNullOrEmpty(supabaseProperty.Status))
        {
            Enum.TryParse(supabaseProperty.Status, true, out status);
        }

        // Get first image URL from ImageUrls array, or use ImageUrl as fallback
        string? imageUrl = supabaseProperty.ImageUrl;
        if (supabaseProperty.ImageUrls != null && supabaseProperty.ImageUrls.Count > 0)
        {
            imageUrl = supabaseProperty.ImageUrls[0];
        }

        // Convert features dictionary to list of feature names where value is true
        List<string>? features = null;
        if (supabaseProperty.FeaturesDict != null)
        {
            features = supabaseProperty.FeaturesDict
                .Where(kvp => {
                    // Check if value is true (can be bool or string "true")
                    if (kvp.Value is bool boolValue)
                        return boolValue;
                    if (kvp.Value is string strValue)
                        return strValue.Equals("true", StringComparison.OrdinalIgnoreCase);
                    return false;
                })
                .Select(kvp => kvp.Key)
                .ToList();
        }

        return new Property
        {
            Id = supabaseProperty.Id ?? string.Empty,
            Title = supabaseProperty.Title ?? string.Empty,
            Description = supabaseProperty.Description ?? string.Empty,
            PropertyType = propertyType,
            Price = supabaseProperty.Price,
            Currency = supabaseProperty.Currency ?? "XAF",
            Location = supabaseProperty.Location ?? string.Empty,
            Address = supabaseProperty.Address,
            Area = supabaseProperty.Area,
            Bedrooms = supabaseProperty.Bedrooms,
            Bathrooms = supabaseProperty.Bathrooms,
            ImageUrl = imageUrl,
            ImageUrls = supabaseProperty.ImageUrls,
            Status = status,
            CreatedAt = supabaseProperty.CreatedAt,
            UpdatedAt = supabaseProperty.UpdatedAt,
            OwnerId = supabaseProperty.OwnerId ?? string.Empty,
            Features = features
        };
    }

    private string TranslateError(string message)
    {
        return message.ToLower() switch
        {
            var msg when msg.Contains("network") => "Erreur de connexion. Vérifiez votre internet.",
            var msg when msg.Contains("timeout") => "Le serveur ne répond pas. Réessayez plus tard.",
            var msg when msg.Contains("unauthorized") => "Session expirée. Veuillez vous reconnecter.",
            var msg when msg.Contains("not found") => "Propriété introuvable.",
            _ => "Une erreur s'est produite lors du chargement des propriétés."
        };
    }
}

// Supabase table model (maps to properties table with snake_case columns)
[Table("properties")]
public class SupabaseProperty : BaseModel
{
    [PrimaryKey("id", false)]
    public string? Id { get; set; }

    [Column("title")]
    public string? Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("property_type")]
    public string? PropertyType { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("currency")]
    public string? Currency { get; set; }

    [Column("location")]
    public string? Location { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("area")]
    public decimal? Area { get; set; }

    [Column("bedrooms")]
    public int? Bedrooms { get; set; }

    [Column("bathrooms")]
    public int? Bathrooms { get; set; }

    [Column("image_url")]
    public string? ImageUrl { get; set; }

    [Column("image_urls")]
    public List<string>? ImageUrls { get; set; }

    [Column("status")]
    public string? Status { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("owner_id")]
    public string? OwnerId { get; set; }

    [Column("features")]
    public Dictionary<string, object>? FeaturesDict { get; set; }
}
