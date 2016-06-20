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
			ShiftDays(book, sheetName);
			ShiftDates(book, sheetName);

			var str = sheet.Row(4).Cell(33).Value.ToString().Split(',').ToList()[0];

			Console.WriteLine(DateTime.Parse(str));

			book.Save();

		}

		public static void ShiftDays(XLWorkbook book, string sheetName)
		{
			var sheet = book.Worksheets.Worksheet(sheetName);

			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
				for (int j = FirstColumnNum; j < FirstColumnNum + DaysCount; j++)
					sheet.Row(i).Cell(j).Value = sheet.Row(i).Cell(j + 1);

			book.Save();
		}

		public static void ShiftDates(XLWorkbook book, string sheetName)
		{
			var sheet = book.Worksheets.Worksheet(sheetName);

			var dayNum =	int.Parse(sheet.Row(FirstStringNum - 1).Cell(FirstColumnNum + DaysCount - 1).Value.ToString());
			var monthNum =	int.Parse(sheet.Row(FirstStringNum - 2).Cell(FirstColumnNum + DaysCount - 1).Value.ToString());
			var yearNum =	int.Parse(sheet.Row(1).Cell(1).Value.ToString());

			var lastDate  = new DateTime(yearNum, monthNum, dayNum);
			var newLastDate = lastDate.AddDays(1);

			for (int i = FirstStringNum - 2; i < FirstStringNum; i++)
				for (int j = FirstColumnNum; j < FirstColumnNum + DaysCount; j++)
					sheet.Row(i).Cell(j).Value = sheet.Row(i).Cell(j + 1);

			sheet.Row(FirstStringNum - 1).Cell(FirstColumnNum + DaysCount - 1).Value = newLastDate.Day;
			sheet.Row(FirstStringNum - 2).Cell(FirstColumnNum + DaysCount - 1).Value = newLastDate.Month;
			sheet.Row(1).Cell(1).Value = newLastDate.Year;

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
