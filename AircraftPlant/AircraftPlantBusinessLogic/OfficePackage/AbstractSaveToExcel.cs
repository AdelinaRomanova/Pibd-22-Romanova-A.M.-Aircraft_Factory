﻿using AircraftPlantBusinessLogic.OfficePackage.HelperEnums;
using AircraftPlantBusinessLogic.OfficePackage.HelperModels;

namespace AircraftPlantBusinessLogic.OfficePackage
{
	public abstract class AbstractSaveToExcel
	{
		/// Создание отчета
		public void CreateReport(ExcelInfo info)
		{
			CreateExcel(info);
			InsertCellInWorksheet(new ExcelCellParameters
			{
				ColumnName = "A",
				RowIndex = 1,
				Text = info.Title,
				StyleInfo = ExcelStyleInfoType.Title
			});
			MergeCells(new ExcelMergeParameters
			{
				CellFromName = "A1",
				CellToName = "C1"
			});
			uint rowIndex = 2;
			foreach (var plane in info.PlaneComponents)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = plane.PlaneName,
					StyleInfo = ExcelStyleInfoType.Text
				});
				rowIndex++;
				foreach (var component in plane.Components)
				{
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "B",
						RowIndex = rowIndex,
						Text = component.Item1,
						StyleInfo = ExcelStyleInfoType.TextWithBroder
					});
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "C",
						RowIndex = rowIndex,
						Text = component.Item2.ToString(),
						StyleInfo = ExcelStyleInfoType.TextWithBroder
					});
					rowIndex++;
				}
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "C",
					RowIndex = rowIndex,
					Text = plane.TotalCount.ToString(),
					StyleInfo = ExcelStyleInfoType.Text
				});
				rowIndex++;
			}
			SaveExcel(info);
		}

		// Создание excel-файла
		protected abstract void CreateExcel(ExcelInfo info);
		/// Добавляем новую ячейку в лист
		protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);
		/// Объединение ячеек
		protected abstract void MergeCells(ExcelMergeParameters excelParams);
		/// Сохранение файла
		protected abstract void SaveExcel(ExcelInfo info);
	}
}