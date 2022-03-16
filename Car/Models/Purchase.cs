using System;
using System.Collections.Generic;

namespace Car.Models
{
    public partial class Purchase
    {
        public Guid Purchaseid { get; set; }
        public Guid Carid { get; set; }
        public Guid Userid { get; set; }
        public DateTime? Purchasedate { get; set; }

        public virtual Car? Car { get; set; } = null!;
        public virtual User? User { get; set; } = null!;
    }
}
