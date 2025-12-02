using Wintakam.Models;

namespace Wintakam.Services;

public interface IPropertyService
{
    Task<List<Property>> GetAllPropertiesAsync();
    Task<Property?> GetPropertyByIdAsync(string id);
    Task<List<Property>> GetPropertiesByOwnerAsync(string ownerId);
}
