﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab_62_Hospital_WPF_Framework
{
    public class Cleaner
    {
        public int CleanerId { get; set; }
        public string CleanerName { get; set; }
        public int? HospitalId { get; set; }

        public Hospital Hospital { get; set; }
    }
}
