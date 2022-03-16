﻿using System;

namespace AircraftPlantContracts.BindingModels
{
	public class ReportOrdersViewModel
	{
		public DateTime DateCreate { get; set; }
		public string PlaneName { get; set; }
		public int Count { get; set; }
		public decimal Sum { get; set; }
		public string Status { get; set; }
	}
}