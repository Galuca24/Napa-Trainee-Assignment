﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Port
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public ICollection<Voyage> DepartureVoyages { get; set; } = new List<Voyage>();
        public ICollection<Voyage> ArrivalVoyages { get; set; } = new List<Voyage>();
    }

}
