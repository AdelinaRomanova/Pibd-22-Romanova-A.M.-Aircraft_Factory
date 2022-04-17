﻿using System;
using System.Collections.Generic;
using AircraftPlantContracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace AircraftPlantDatabaseImplement.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int ClientId { get; set; }

		public int PlaneId { get; set; }

		[Required]
		public int Count { get; set; }

		[Required]
		public decimal Sum { get; set; }

		public OrderStatus Status { get; set; }

		[Required]
		public DateTime DateCreate { get; set; }

		public DateTime? DateImplement { get; set; }

		public virtual Plane Planes { get; set; }
		public virtual Client Client { get; set; }

	}
}
