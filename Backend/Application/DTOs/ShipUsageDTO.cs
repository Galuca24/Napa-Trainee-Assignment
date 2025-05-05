using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ShipUsageDTO
    {
        public string ShipName { get; set; } = string.Empty;
        public int VoyageCount { get; set; }
    }

}
