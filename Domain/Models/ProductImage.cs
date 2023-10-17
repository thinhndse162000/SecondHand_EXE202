using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class ProductImage
    {
        public Guid ImageId { get; set; }
        public string? ImageUrl { get; set; }
        public int? CaroselOrder { get; set; }
        public Guid ProductId { get; set; }

        public  Product? Product { get; set; } 
    }
}
