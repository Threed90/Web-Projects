using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ProductCatalog.Models.DTO
{
    public class ProductDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [MinLength(2)]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Range(0.01, 100_000.00)]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [Range(0, 1_000)]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
