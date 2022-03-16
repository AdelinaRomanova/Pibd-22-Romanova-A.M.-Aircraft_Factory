﻿using AircraftPlantContracts.BindingModels;
using AircraftPlantContracts.StoragesContracts;
using AircraftPlantContracts.ViewModels;
using AircraftPlantDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AircraftPlantDatabaseImplement.Implements
{
	public class PlaneStorage : IPlaneStorage
	{
		public List<PlaneViewModel> GetFullList()
		{
			using var context = new AircraftPlantDatabase();
			return context.Planes
			.Include(rec => rec.PlaneComponents)
			.ThenInclude(rec => rec.Component)
			.ToList()
			.Select(CreateModel)
			.ToList();
		}

		public List<PlaneViewModel> GetFilteredList(PlaneBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			using var context = new AircraftPlantDatabase();
			return context.Planes
			.Include(rec => rec.PlaneComponents)
			.ThenInclude(rec => rec.Component)
			.Where(rec => rec.PlaneName.Contains(model.PlaneName))
			.ToList()
			.Select(CreateModel)
			.ToList();
		}

		public PlaneViewModel GetElement(PlaneBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			using var context = new AircraftPlantDatabase();
			var plane = context.Planes
			.Include(rec => rec.PlaneComponents)
			.ThenInclude(rec => rec.Component)
			.FirstOrDefault(rec => rec.PlaneName == model.PlaneName || rec.Id == model.Id);
			return plane != null ? CreateModel(plane) : null;
		}

		public void Insert(PlaneBindingModel model)
		{
			using var context = new AircraftPlantDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				Plane plane = new Plane()
				{
					PlaneName = model.PlaneName,
					Price = model.Price
				};
				context.Planes.Add(plane);
				context.SaveChanges();
				CreateModel(model, plane, context);
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Update(PlaneBindingModel model)
		{
			using var context = new AircraftPlantDatabase();
			using var transaction = context.Database.BeginTransaction();
			try
			{
				var element = context.Planes.FirstOrDefault(rec => rec.Id == model.Id);
				if (element == null)
				{
					throw new Exception("Элемент не найден");
				}
				CreateModel(model, element, context);
				context.SaveChanges();
				transaction.Commit();
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public void Delete(PlaneBindingModel model)
		{
			using var context = new AircraftPlantDatabase();
			Plane element = context.Planes.FirstOrDefault(rec => rec.Id == model.Id);
			if (element != null)
			{
				context.Planes.Remove(element);
				context.SaveChanges();
			}
			else
			{
				throw new Exception("Элемент не найден");
			}
		}

		private static Plane CreateModel(PlaneBindingModel model, Plane plane, AircraftPlantDatabase context)
		{
			plane.PlaneName = model.PlaneName;
			plane.Price = model.Price;
			if (model.Id.HasValue)
			{
				var planeComponents = context.PlaneComponents.Where(rec => rec.PlaneId == model.Id.Value).ToList();
				// удалили те, которых нет в модели
				context.PlaneComponents.RemoveRange(planeComponents.Where(rec => !model.PlaneComponents.ContainsKey(rec.ComponentId)).ToList()); 
				context.SaveChanges();
				// обновили количество у существующих записей
				foreach (var updateComponent in planeComponents)
				{
					updateComponent.Count = model.PlaneComponents[updateComponent.ComponentId].Item2;
					model.PlaneComponents.Remove(updateComponent.ComponentId);
				}
				context.SaveChanges();
			}
			// добавили новые
			foreach (var pc in model.PlaneComponents)
			{
				context.PlaneComponents.Add(new PlaneComponent
				{
					PlaneId = plane.Id,
					ComponentId = pc.Key,
					Count = pc.Value.Item2
				});
				context.SaveChanges();
			}
			return plane;
		}

		private static PlaneViewModel CreateModel(Plane plane)
		{
			return new PlaneViewModel
			{
				Id = plane.Id,
				PlaneName = plane.PlaneName,
				Price = plane.Price,
				PlaneComponents = plane.PlaneComponents.ToDictionary(recPC => recPC.ComponentId, recPC => (recPC.Component?.ComponentName, recPC.Count))
			};
		}
	}
}
