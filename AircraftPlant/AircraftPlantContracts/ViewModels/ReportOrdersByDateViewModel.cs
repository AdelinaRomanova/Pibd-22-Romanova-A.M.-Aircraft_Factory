﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftPlantContracts.ViewModels
{
    public class ReportOrdersByDateViewModel
    {
        public DateTime DateCreate { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}