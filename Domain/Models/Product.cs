using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
        }

        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Size { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int Status { get; set; }

        public string? Description { get; set; }
        public string? Cond { get; set; }
        public string? Color { get; set; }
        public string? Style { get; set; }
        public string? Brand { get; set; }
        public string? OriginalImage { get; set; }
        public Guid StorageId { get; set; }
        public Guid CategoryId { get; set; }

        public  Category? Category { get; set; } 
        public  Storage? Storage { get; set; } 
        public  ICollection<ProductImage>? ProductImages { get; set; }
    }
}
