using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Domain.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Storages = new HashSet<Storage>();
        }

        
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
        [DefaultValue("User")]
        public string? Role { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Facebook { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        public virtual ICollection<Storage>? Storages { get; set; }
    }
}
