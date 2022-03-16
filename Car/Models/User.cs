using System;
using System.Collections.Generic;
using Car.Entities;
using Newtonsoft.Json;

namespace Car.Models
{
    public partial class User
    {
        public Guid Userid { get; set; }
        public string Useremail { get; set; } = null!;
        public string Userpassword { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Userphno { get; set; }
        public string UserCity { get; set; } = null!;
        public Role UserRole { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
        public virtual ICollection<Purchase>? Purchases { get; set; }
    }
}
