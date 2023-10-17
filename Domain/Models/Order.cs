using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Order
    {
        public Guid OrderId { get; set; }
        public decimal? Total { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? Status { get; set; }
        public int? FeedBackRating { get; set; }
        public string? FeedBackMessage { get; set; }
        public Guid UserId { get; set; }
        public Guid StorageId { get; set; }
        public Guid PaymentId { get; set; }

        public virtual Payment? Payment { get; set; } = null!;
        public virtual Storage? Storage { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
