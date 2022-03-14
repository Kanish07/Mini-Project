using System;
using System.Collections.Generic;

namespace Car.Models
{
    public partial class Car
    {
        public Car()
        {
            Purchases = new HashSet<Purchase>();
        }

        public Guid Carid { get; set; }
        public string Carmake { get; set; } = null!;
        public string Carmodelname { get; set; } = null!;
        public string Carshifttype { get; set; } = null!;
        public string? Carstatus { get; set; }
        public string Cartype { get; set; } = null!;
        public string Carcity { get; set; } = null!;
        public string Carfuel { get; set; } = null!;
        public Guid Userid { get; set; }
        
        public virtual User? User { get; set; } = null!;
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
