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
	public class OrderStorage : IOrderStorage
	{
        public List<OrderViewModel> GetFullList()
        {
            using var context = new AircraftPlantDatabase();
            return context.Orders.
                Include(rec => rec.Planes)
                .Select(CreateModel)
                .ToList();
        }
        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AircraftPlantDatabase();
            return context.Orders.Include(rec => rec.Planes)
                .Where(rec => rec.PlaneId == model.PlaneId)
                .Select(CreateModel)
                .ToList();
        }
        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new AircraftPlantDatabase();
            var order = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }
        public void Insert(OrderBindingModel model)
        {
            using var context = new AircraftPlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.Orders.Add(CreateModel(model, new Order()));
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(OrderBindingModel model)
        {
            using var context = new AircraftPlantDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Заказ не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(OrderBindingModel model)
        {
            using var context = new AircraftPlantDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Заказ не найден");
            }
        }
        public static Order CreateModel(OrderBindingModel model, Order order)
        {
            order.PlaneId = model.PlaneId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }
        public OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                PlaneId = order.PlaneId,
                PlaneName = order.Planes.PlaneName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status.ToString(),
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
