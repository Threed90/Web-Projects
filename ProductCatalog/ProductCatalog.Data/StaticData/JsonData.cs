using Microsoft.Extensions.Configuration;
using ProductCatalog.Models.DTO;
using System.Reflection;
using System.Text.Json;

namespace ProductCatalog.Data.StaticData
{
    public static class JsonData
    {
        public static List<ProductDTO> GetData(string fileName)
        {
            var basePath = Assembly.GetCallingAssembly().Location;
            var text = File.ReadAllText($"../ProductCatalog.Data/StaticData/{fileName}.json");

            var dtos = JsonSerializer.Deserialize<List<ProductDTO>>(text);

            return dtos ?? new List<ProductDTO>();
        }
    }
}
