﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateShipDTO
    {
        public string Name { get; set; } = string.Empty;
        public double MaxSpeed { get; set; }
    }
}
