﻿using AircraftPlantContracts.BindingModels;
using AircraftPlantContracts.StoragesContracts;
using AircraftPlantContracts.ViewModels;
using AircraftPlantFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AircraftPlantFileImplement.Implements
{
	public class PlaneStorage : IPlaneStorage
	{
		private readonly FileDataListSingleton source;
		public PlaneStorage()
		{
			source = FileDataListSingleton.GetInstance();
		}
		public List<PlaneViewModel> GetFullList()
		{
			return source.Planes.Select(CreateModel).ToList();
		}
		public List<PlaneViewModel> GetFilteredList(PlaneBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return source.Planes.Where(rec => rec.PlaneName.Contains(model.PlaneName)).Select(CreateModel).ToList();
		}
		public PlaneViewModel GetElement(PlaneBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			var plane = source.Planes.FirstOrDefault(rec => rec.PlaneName == model.PlaneName || rec.Id == model.Id);
			return plane != null ? CreateModel(plane) : null;
		}
		public void Insert(PlaneBindingModel model)
		{
			int maxId = source.Planes.Count > 0 ? source.Components.Max(rec => rec.Id) : 0;
			var element = new Plane
			{
				Id = maxId + 1,
				PlaneComponents = new Dictionary<int, int>()
			};
			source.Planes.Add(CreateModel(model, element));
		}
		public void Update(PlaneBindingModel model)
		{
			var element = source.Planes.FirstOrDefault(rec => rec.Id == model.Id);
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			CreateModel(model, element);
		}
		public void Delete(PlaneBindingModel model)
		{
			Plane element = source.Planes.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				source.Planes.Remove(element);
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}
		private static Plane CreateModel(PlaneBindingModel model, Plane plane)
		{
			plane.PlaneName = model.PlaneName;
			plane.Price = model.Price;
			// удаляем убранные
			foreach (var key in plane.PlaneComponents.Keys.ToList())
			{
				if (!model.PlaneComponents.ContainsKey(key))
				{
					plane.PlaneComponents.Remove(key);
				}
			}
			// обновляем существуюущие и добавляем новые
			foreach (var component in model.PlaneComponents)
			{
				if (plane.PlaneComponents.ContainsKey(component.Key))
				{
					plane.PlaneComponents[component.Key] = model.PlaneComponents[component.Key].Item2;
				}
				else
				{
					plane.PlaneComponents.Add(component.Key, model.PlaneComponents[component.Key].Item2);
				}
			}
			return plane;
		}
		private PlaneViewModel CreateModel(Plane plane)
		{
			return new PlaneViewModel
			{
				Id = plane.Id,
				PlaneName = plane.PlaneName,
				Price = plane.Price,
				PlaneComponents = plane.PlaneComponents.ToDictionary(recPC => recPC.Key, recPC => (source.Components.FirstOrDefault(recC => recC.Id == recPC.Key)?.ComponentName, recPC.Value))
			};
		}
	}
}