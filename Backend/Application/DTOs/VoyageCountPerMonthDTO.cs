using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class VoyageCountPerMonthDTO
    {
        public string Month { get; set; } = string.Empty; // format YYYY-MM
        public int Count { get; set; }
    }

}
