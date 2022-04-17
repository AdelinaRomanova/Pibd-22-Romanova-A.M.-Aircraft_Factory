﻿using AircraftPlantContracts.BindingModels;
using AircraftPlantContracts.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AircraftPlantContracts.StoragesContracts
{
	public interface IClientStorage
	{
		List<ClientViewModel> GetFullList();
		List<ClientViewModel> GetFilteredList(ClientBindingModel model);
		ClientViewModel GetElement(ClientBindingModel model);
		void Insert(ClientBindingModel model);
		void Update(ClientBindingModel model);
		void Delete(ClientBindingModel model);
	}
}
