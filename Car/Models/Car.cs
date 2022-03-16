using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        public int Carprice { get; set; }
        public Guid Userid { get; set; }
        
        public virtual User? User { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
