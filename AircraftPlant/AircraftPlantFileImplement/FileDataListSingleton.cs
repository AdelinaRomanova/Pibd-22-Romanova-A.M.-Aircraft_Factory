﻿using AircraftPlantFileImplement.Models;
using AircraftPlantContracts.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AircraftPlantFileImplement
{
	public class FileDataListSingleton
	{
		private static FileDataListSingleton instance;
		private readonly string ComponentFileName = "Component.xml";
		private readonly string OrderFileName = "Order.xml";
		private readonly string PlaneFileName = "Plane.xml";
		public List<Component> Components { get; set; }
		public List<Order> Orders { get; set; }
		public List<Plane> Planes { get; set; }
		private FileDataListSingleton()
		{
			Components = LoadComponents();
			Orders = LoadOrders();
			Planes = LoadPlanes();
		}
		public static FileDataListSingleton GetInstance()
		{
			if (instance == null)
			{
				instance = new FileDataListSingleton();
			}
			return instance;
		}
		~FileDataListSingleton()
		{
			SaveComponents();
			SaveOrders();
			SavePlanes();
		}
		private List<Component> LoadComponents()
		{
			var list = new List<Component>();
			if (File.Exists(ComponentFileName))
			{
				var xDocument = XDocument.Load(ComponentFileName);
				var xElements = xDocument.Root.Elements("Component").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Component
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						ComponentName = elem.Element("ComponentName").Value
					});
				}
			}
			return list;
		}
		private List<Order> LoadOrders()
		{
			var list = new List<Order>();
			if (File.Exists(OrderFileName))
			{
				var xDocument = XDocument.Load(OrderFileName);
				var xElements = xDocument.Root.Elements("Order").ToList();
				foreach (var elem in xElements)
				{
					list.Add(new Order
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						PlaneId = Convert.ToInt32(elem.Element("PlaneId").Value),
						Count = Convert.ToInt32(elem.Element("Count").Value),
						Sum = Convert.ToInt32(elem.Element("Sum").Value),
						Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
						DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
						DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ?
						(DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value)
					});
				}
			}
			return list;
		}
		private List<Plane> LoadPlanes()
		{
			var list = new List<Plane>();
			if (File.Exists(PlaneFileName))
			{
				var xDocument = XDocument.Load(PlaneFileName);
				var xElements = xDocument.Root.Elements("Plane").ToList();
				foreach (var elem in xElements)
				{
					var planeComp = new Dictionary<int, int>();
					foreach (var component in elem.Element("PlaneComponents").Elements("PlaneComponent").ToList())
					{
						planeComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
					}
					list.Add(new Plane
					{
						Id = Convert.ToInt32(elem.Attribute("Id").Value),
						PlaneName = elem.Element("PlaneName").Value,
						Price = Convert.ToDecimal(elem.Element("Price").Value),
						PlaneComponents = planeComp
					});
				}
			}
			return list;
		}
		private void SaveComponents()
		{
			if (Components != null)
			{
				var xElement = new XElement("Components");
				foreach (var component in Components)
				{
					xElement.Add(new XElement("Component", new XAttribute("Id", component.Id), new XElement("ComponentName", component.ComponentName)));
				}
				var xDocument = new XDocument(xElement);
				xDocument.Save(ComponentFileName);
			}
		}
		private void SaveOrders()
		{
			if (Orders != null)
			{
				var xElement = new XElement("Orders");
				foreach (var order in Orders)
				{
					xElement.Add(new XElement("Order",
					new XAttribute("Id", order.Id),
					new XElement("PlaneId", order.PlaneId),
					new XElement("Count", order.Count),
					new XElement("Sum", order.Sum),
					new XElement("Status", order.Status),
					new XElement("DateCreate", order.DateCreate),
					new XElement("DateImplement", order.DateImplement)));
				}
				XDocument xDocument = new XDocument(xElement);
				xDocument.Save(OrderFileName);
			}
		}
		private void SavePlanes()
		{
			if (Planes != null)
			{
				var xElement = new XElement("Planes");
				foreach (var plane in Planes)
				{
					var compElement = new XElement("PlaneComponents");
					foreach (var component in plane.PlaneComponents)
					{
						compElement.Add(new XElement("PlaneComponent",
						new XElement("Key", component.Key),
						new XElement("Value", component.Value)));
					}
					xElement.Add(new XElement("Plane",
					new XAttribute("Id", plane.Id),
					new XElement("PlaneName", plane.PlaneName),
					new XElement("Price", plane.Price),
					compElement));
				}
				var xDocument = new XDocument(xElement);
				xDocument.Save(PlaneFileName);
			}
		}
		public static void Save()
		{
			instance.SaveOrders();
			instance.SavePlanes();
			instance.SaveComponents();
		}
	}
}
