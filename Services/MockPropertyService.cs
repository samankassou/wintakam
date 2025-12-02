using Wintakam.Models;

namespace Wintakam.Services;

public class MockPropertyService : IPropertyService
{
    private readonly List<Property> _properties;

    public MockPropertyService()
    {
        _properties = new List<Property>
        {
            new Property
            {
                Id = "1",
                Title = "Belle villa moderne à Bonanjo",
                Description = "Magnifique villa de 4 chambres avec piscine et jardin. Vue sur mer exceptionnelle.",
                PropertyType = PropertyType.Maison,
                Price = 85000000,
                Currency = "XAF",
                Location = "Bonanjo, Douala",
                Address = "Avenue de la Liberté, Bonanjo",
                Area = 250,
                Bedrooms = 4,
                Bathrooms = 3,
                ImageUrl = "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800",
                Status = PropertyStatus.Disponible,
                CreatedAt = DateTime.Now.AddDays(-10),
                OwnerId = "user1",
                Features = new List<string> { "Piscine", "Jardin", "Garage", "Climatisation" }
            },
            new Property
            {
                Id = "2",
                Title = "Appartement 3 pièces à Akwa",
                Description = "Appartement spacieux au cœur d'Akwa, proche de toutes commodités.",
                PropertyType = PropertyType.Appartement,
                Price = 35000000,
                Currency = "XAF",
                Location = "Akwa, Douala",
                Address = "Rue Joffre, Akwa",
                Area = 120,
                Bedrooms = 3,
                Bathrooms = 2,
                ImageUrl = "https://images.unsplash.com/photo-1522708323590-d24dbb6b0267?w=800",
                Status = PropertyStatus.Disponible,
                CreatedAt = DateTime.Now.AddDays(-5),
                OwnerId = "user2",
                Features = new List<string> { "Parking", "Ascenseur", "Balcon" }
            },
            new Property
            {
                Id = "3",
                Title = "Terrain à bâtir - Logbessou",
                Description = "Terrain de 500m² dans un quartier résidentiel calme. Viabilisé et prêt à construire.",
                PropertyType = PropertyType.Terrain,
                Price = 15000000,
                Currency = "XAF",
                Location = "Logbessou, Douala",
                Area = 500,
                ImageUrl = "https://images.unsplash.com/photo-1500382017468-9049fed747ef?w=800",
                Status = PropertyStatus.Disponible,
                CreatedAt = DateTime.Now.AddDays(-15),
                OwnerId = "user1",
                Features = new List<string> { "Viabilisé", "Clôturé", "Accès goudronné" }
            },
            new Property
            {
                Id = "4",
                Title = "Duplex haut standing à Bonapriso",
                Description = "Duplex luxueux de 5 chambres avec finitions haut de gamme.",
                PropertyType = PropertyType.Maison,
                Price = 120000000,
                Currency = "XAF",
                Location = "Bonapriso, Douala",
                Address = "Boulevard de la République, Bonapriso",
                Area = 300,
                Bedrooms = 5,
                Bathrooms = 4,
                ImageUrl = "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800",
                Status = PropertyStatus.Disponible,
                CreatedAt = DateTime.Now.AddDays(-3),
                OwnerId = "user3",
                Features = new List<string> { "Piscine", "Salle de sport", "Garage double", "Système de sécurité" }
            },
            new Property
            {
                Id = "5",
                Title = "Studio meublé à Bali",
                Description = "Studio tout équipé idéal pour étudiant ou jeune professionnel.",
                PropertyType = PropertyType.Appartement,
                Price = 12000000,
                Currency = "XAF",
                Location = "Bali, Douala",
                Area = 35,
                Bedrooms = 1,
                Bathrooms = 1,
                ImageUrl = "https://images.unsplash.com/photo-1554995207-c18c203602cb?w=800",
                Status = PropertyStatus.Disponible,
                CreatedAt = DateTime.Now.AddDays(-7),
                OwnerId = "user2",
                Features = new List<string> { "Meublé", "Wifi", "Eau et électricité inclus" }
            }
        };
    }

    public async Task<List<Property>> GetAllPropertiesAsync()
    {
        // Simulate network delay
        await Task.Delay(1000);
        return _properties;
    }

    public async Task<Property?> GetPropertyByIdAsync(string id)
    {
        // Simulate network delay
        await Task.Delay(500);
        return _properties.FirstOrDefault(p => p.Id == id);
    }

    public async Task<List<Property>> GetPropertiesByOwnerAsync(string ownerId)
    {
        // Simulate network delay
        await Task.Delay(800);
        return _properties.Where(p => p.OwnerId == ownerId).ToList();
    }
}
