using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CateImage { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
