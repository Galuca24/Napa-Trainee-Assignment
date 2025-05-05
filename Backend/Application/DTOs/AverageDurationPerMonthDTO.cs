using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AverageDurationPerMonthDTO
    {
        public string Month { get; set; } = string.Empty; // format: "YYYY-MM"
        public double AverageDuration { get; set; }
    }

}
