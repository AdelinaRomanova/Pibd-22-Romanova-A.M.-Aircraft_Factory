﻿using System.Runtime.Serialization;

namespace AircraftPlantContracts.BindingModels
{
    public class ImplementerBindingModel
    {
		public int? Id { get; set; }
		public string ImplementerFIO { get; set; }
		public int WorkingTime { get; set; }
		public int PauseTime { get; set; }
	}
}