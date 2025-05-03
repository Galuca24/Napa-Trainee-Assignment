using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ship
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double MaxSpeed { get; set; }

        public ICollection<Voyage> Voyages { get; set; } = new List<Voyage>();
    }

}
