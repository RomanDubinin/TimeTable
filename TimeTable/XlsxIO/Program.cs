using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using TimeTable;

namespace XlsxIO
{
	class Program
	{
		private static int firstStringNum = 6;
		private static int firstColumnNum = 2;

		private static int WorkersCount = 14;
		private static int DaysCount = 29;

		static void Main(string[] args)
		{
			var filename = @"Копия Вахты-хуяхты.xlsx";
			var listname = "Февраль";

			var wishTable = ReadTableFromXlsx(filename, listname, WorkersCount, DaysCount);
			var algo = new Algotithm();

			algo.RecursiveAlgo(WorkTable.CreateEmpty(WorkersCount, DaysCount), wishTable, 10);

			for (int i = 0; i < algo.GeneratedTables.Count; i++)
			{
				string newListName = $"{listname} attemption {i + 1}";
				CreateListCopy(filename, listname, newListName);

				WriteWorkTableToXlsx(filename, newListName, algo.GeneratedTables[i]);
			}
		}

		private static void WriteWorkTableToXlsx(string xlsxFilename, string listName, WorkTable workTable)
		{
			var book = new XLWorkbook(xlsxFilename);
			var sheet = book.Worksheets.Worksheet(listName);

			var wishMatrix = new List<List<WishTableCell>>();

			for (int i = 0; i < workTable.RowsCount; i++)
			{
				wishMatrix.Add(new List<WishTableCell>());
				for (int j = 0; j < workTable.ColumnsCount; j++)
				{
					if (workTable[i, j] == WorkTableCell.Work)
						sheet.Row(i + firstStringNum).Cell(j + firstColumnNum).Value = "4";
				}
			}

			book.Save();
		}

		public static WishTable ReadTableFromXlsx(string filename, string listname, int workersCount, int daysCount)
		{
			var book = new XLWorkbook(filename);
			var sheet = book.Worksheets.Worksheet(listname);

			var wishMatrix = new List<List<WishTableCell>>();

			for (int i = 0; i < workersCount; i++)
			{
				wishMatrix.Add(new List<WishTableCell>());
				for (int j = 0; j < daysCount; j++)
				{
					if (sheet.Row(i + firstStringNum).Cell(j + firstColumnNum).Style.Fill.BackgroundColor == XLColor.Lime)
						wishMatrix[i].Add(WishTableCell.Yes);

					else if (sheet.Row(i + firstStringNum).Cell(j + firstColumnNum).Style.Fill.BackgroundColor == XLColor.Red)
						wishMatrix[i].Add(WishTableCell.No);

					else
						wishMatrix[i].Add(WishTableCell.Empty);
				}
			}

			return new WishTable(wishMatrix);
		}

		public static void CreateListCopy(string xlsxFilename, string originalListName, string copyName)
		{
			var book = new XLWorkbook(xlsxFilename);
			var sheet = book.Worksheets.Worksheet(originalListName);
			try
			{
				book.Worksheets.Delete(copyName); 
			}
			catch (KeyNotFoundException)//sheet is not exists
			{ }
			sheet.CopyTo(copyName);
			book.Save();
		}
	}
}
