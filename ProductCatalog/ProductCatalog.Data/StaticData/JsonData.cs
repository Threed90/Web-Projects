using Microsoft.Extensions.Configuration;
using ProductCatalog.Models.DTO;
using System.Text.Json;

namespace ProductCatalog.Data.StaticData
{
    public static class JsonData
    {
        public static List<ProductDTO> GetData(string fileName)
        {
            var text = File.ReadAllText($"./StaticData/{fileName}.json");

            var dtos = JsonSerializer.Deserialize<List<ProductDTO>>(text);

            return dtos ?? new List<ProductDTO>();
        }
    }
}
