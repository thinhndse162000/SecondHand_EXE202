using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public Guid PaymentId { get; set; }
        public string? Method { get; set; }
        public int? Status { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
