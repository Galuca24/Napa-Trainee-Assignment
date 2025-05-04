using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VisitedCountryDTO
    {
        public string Country { get; set; } = string.Empty;
        public Guid ShipId { get; set; }
        public Guid VoyageId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }



}
