using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using TimeTable;

namespace XlsxIO
{
	class Program
	{
		private static int FirstStringNum = 6;
		private static int FirstColumnNum = 2;

		private static int WorkersCount = 14;
		private static int DaysCount = 31;

		static void Main(string[] args)
		{
			var filename = @"C:\Users\Roman_000\Downloads\Вахты.xlsx";
			var listname = "Март";

			var wishTable = ReadTableFromXlsx(filename, listname);
			var mainTable = MainTable.FromTtwoTables(WorkTable.CreateEmpty(WorkersCount, DaysCount), wishTable, 2);

			var algo = new Algotithm();

			algo.RecursiveAlgo(mainTable, 10);

			for (int i = 0; i < algo.GeneratedTables.Count; i++)
			{
				string newListName = $"{listname} filled {i + 1}";
				CreateListCopy(filename, listname, newListName);

				WriteWorkTableToXlsx(filename, newListName, algo.GeneratedTables[i].GetWorkTable);
			}
		}

		private static void WriteWorkTableToXlsx(string xlsxFilename, string listName, WorkTable workTable)
		{
			var book = new XLWorkbook(xlsxFilename);
			var sheet = book.Worksheets.Worksheet(listName);

			var wishMatrix = new List<List<WishTableCell>>();

			for (int i = 0; i < workTable.Matrix.Count; i++)
			{
				wishMatrix.Add(new List<WishTableCell>());
				for (int j = 0; j < workTable.Matrix[i].Count; j++)
				{
					if (workTable.Matrix[i][j] == WorkTableCell.Work)
						sheet.Row(i + FirstStringNum).Cell(j + FirstColumnNum).Value = "4";
				}
			}

			book.Save();
		}

		public static WishTable ReadTableFromXlsx(string filename, string listname)
		{
			var book = new XLWorkbook(filename);
			var sheet = book.Worksheets.Worksheet(listname);

			var wishMatrix = new List<List<WishTableCell>>();

			for (int i = 0; i < WorkersCount; i++)
			{
				wishMatrix.Add(new List<WishTableCell>());
				for (int j = 0; j < DaysCount; j++)
				{
					if (sheet.Row(i + FirstStringNum).Cell(j + FirstColumnNum).Style.Fill.BackgroundColor == XLColor.Lime)
						wishMatrix[i].Add(WishTableCell.Yes);

					else if (sheet.Row(i + FirstStringNum).Cell(j + FirstColumnNum).Style.Fill.BackgroundColor == XLColor.Red)
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

		public void WriteTimeTableToFile(string filename, WorkTable workTtable, WishTable wishTable)
		{
			using (StreamWriter writer = new StreamWriter(filename + ".txt"))
			{
				for (int i = 0; i < workTtable.Matrix.Count; i++)
				{
					writer.Write("{0, 2}|", i);
					for (int j = 0; j < workTtable.Matrix[i].Count; j++)
					{
						if (workTtable.Matrix[i][j] == WorkTableCell.Work)
							writer.Write("#");
						else if (wishTable.Matrix[i][j] == WishTableCell.No)
							writer.Write("-");
						else if (wishTable.Matrix[i][j] == WishTableCell.Yes && workTtable.Matrix[i][j] != WorkTableCell.Work)
							writer.Write("+");
						else
							writer.Write(" ");
					}
					writer.Write("  |" + workTtable.CountOfWorkDays(i));
					writer.Write('\n');
				}
			}
		}
	}
}
