using System;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using OfficeOpenXml;

namespace XlsxIO
{
	class Program
	{
		private static int FirstStringNum = 4;
		private static int FirstColumnNum = 2;

		private static int WorkersCount = 14;
		private static int DaysCount = 30;
		private static int DistributedDaysCount = 7;

		static void Main(string[] args)
		{
			var filename = @"C:\Users\RomanUser\Google Drive\Скиф\Расписание.xlsx";
			var sheetName = "Лист1";

			var book = new XLWorkbook(filename);
			var sheet = book.Worksheets.Worksheet(sheetName);
			try
			{
				book.Worksheets.Add(DateTime.Now.Month.ToString());
			}
			catch (System.ArgumentException)
			{
				
			}
			DropNearestDay(book, sheetName);

			var str = sheet.Row(4).Cell(33).Value.ToString().Split(',').ToList()[0];

			Console.WriteLine(DateTime.Parse(str));

			book.Save();

		}

		public static void DropNearestDay(ClosedXML.Excel.XLWorkbook book, string sheetName)
		{
			var sheet = book.Worksheets.Worksheet(sheetName);

			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
				for (int j = FirstColumnNum; j < FirstColumnNum + DaysCount; j++)
					sheet.Row(i).Cell(j).Value = sheet.Row(i).Cell(j + 1);

			book.Save();
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
