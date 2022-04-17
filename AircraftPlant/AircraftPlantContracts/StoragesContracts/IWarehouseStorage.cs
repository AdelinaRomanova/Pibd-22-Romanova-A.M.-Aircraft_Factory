﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AircraftPlantContracts.BindingModels;
using AircraftPlantContracts.ViewModels;

namespace AircraftPlantContracts.StoragesContracts
{
	public interface IWarehouseStorage
	{
        List<WarehouseViewModel> GetFullList();
        List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model);
        WarehouseViewModel GetElement(WarehouseBindingModel model);
        void Insert(WarehouseBindingModel model);
        void Update(WarehouseBindingModel model);
        void Delete(WarehouseBindingModel model);
        bool CheckComponentsCount(int count, Dictionary<int, (string, int)> components);
    }
}