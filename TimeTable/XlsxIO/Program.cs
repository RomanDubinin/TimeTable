using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using TimeTable;

namespace XlsxIO
{
	class Program
	{
		private static int WorkersCount = 14;
		private static int DaysCount = 29;

		static void Main(string[] args)
		{
			var filename = @"Копия Вахты-хуяхты.xlsx";
			var listname = "Февраль";

			var wishTable = ReadTableFromXlsx(filename, listname, WorkersCount, DaysCount);
			var algo = new Algotithm();

			algo.RecursiveAlgo(WorkTable.CreateEmpty(WorkersCount, DaysCount), wishTable, 10);


			Console.WriteLine(123);
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
					if (sheet.Row(i + 6).Cell(j + 2).Style.Fill.BackgroundColor == XLColor.Lime)
						wishMatrix[i].Add(WishTableCell.Yes);

					else if (sheet.Row(i + 6).Cell(j + 2).Style.Fill.BackgroundColor == XLColor.Red)
						wishMatrix[i].Add(WishTableCell.No);

					else
						wishMatrix[i].Add(WishTableCell.Empty);
				}
			}

			return new WishTable(wishMatrix);
		}
	}
}
