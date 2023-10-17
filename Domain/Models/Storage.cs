using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Storage
    {
        public Storage()
        {
            Orders = new HashSet<Order>();
            Products = new HashSet<Product>();
        }

        public Guid StorageId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
