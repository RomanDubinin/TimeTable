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

		private static String DynamicListName = "Лист1";

		static void Main(string[] args)
		{
			var filename = @"C:\Users\RomanUser\Google Drive\Скиф\Расписание.xlsx";

			var book = new XLWorkbook(filename);
			var sheet = book.Worksheets.Worksheet(DynamicListName);
			
			var date = GetDate(sheet, 1);

			CreatePatternCopyIfNotExist(book, "Шаблон", date);

			CopyNearestDayToStaticTable(book, DynamicListName, date.ToString("MMMM"));

			ShiftDays(book, DynamicListName);
			ShiftDates(book, DynamicListName);

			var str = sheet.Row(4).Cell(33).Value.ToString().Split(',').ToList()[0];

			Console.WriteLine(DateTime.Parse(str));

			book.Save();

		}

		private static void CopyNearestDayToStaticTable(XLWorkbook book, string dynamicTableName, string staticTableName)
		{
			var dynamicSheet = book.Worksheets.Worksheet(dynamicTableName);
			var staticSheet = book.Worksheets.Worksheet(staticTableName);
			var day = int.Parse(dynamicSheet.Cell(FirstStringNum - 1, FirstColumnNum).Value.ToString());

			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
			{
				staticSheet.Cell(i, FirstColumnNum + day - 1).Value = dynamicSheet.Cell(i, FirstColumnNum).Value;
			}
		}

		public static void ShiftDays(XLWorkbook book, string sheetName)
		{
			var sheet = book.Worksheets.Worksheet(sheetName);

			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
				for (int j = FirstColumnNum; j < FirstColumnNum + DaysCount; j++)
					sheet.Row(i).Cell(j).Value = sheet.Row(i).Cell(j + 1);
		}

		public static void ShiftDates(XLWorkbook book, string sheetName)
		{
			var sheet = book.Worksheets.Worksheet(sheetName);

			var lastDate  = GetDate(sheet, DaysCount);
			var newLastDate = lastDate.AddDays(1);

			for (int i = FirstStringNum - 2; i < FirstStringNum; i++)
				for (int j = FirstColumnNum; j < FirstColumnNum + DaysCount; j++)
					sheet.Row(i).Cell(j).Value = sheet.Row(i).Cell(j + 1);

			SetLastDate(sheet, newLastDate);
		}

		private static DateTime GetDate(IXLWorksheet sheet, int dayNum)
		{
			var day =	int.Parse(sheet.Row(FirstStringNum - 1).Cell(FirstColumnNum + dayNum - 1).Value.ToString());
			var month = int.Parse(sheet.Row(FirstStringNum - 2).Cell(FirstColumnNum + dayNum - 1).Value.ToString());
			var year =	int.Parse(sheet.Row(1).Cell(1).Value.ToString());

			return new DateTime(year, month, day);
		}

		private static void SetLastDate(IXLWorksheet sheet, DateTime newLastDate)
		{
			sheet.Row(FirstStringNum - 1).Cell(FirstColumnNum + DaysCount - 1).Value = newLastDate.Day;
			sheet.Row(FirstStringNum - 2).Cell(FirstColumnNum + DaysCount - 1).Value = newLastDate.Month;
			sheet.Row(1).Cell(1).Value = newLastDate.Year;
		}

		public static void CreatePatternCopyIfNotExist(XLWorkbook book, string patternName, DateTime date)
		{
			var sheet = book.Worksheets.Worksheet(patternName);
			if (!book.Worksheets.Select(s => s.Name).Contains(date.ToString("MMMM")))
				sheet.CopyTo(date.ToString("MMMM"));

			var newSheet = book.Worksheets.Worksheet(date.ToString("MMMM"));
			newSheet.Row(1).Cell(1).Value = date.Year;
			newSheet.Row(2).Cell(1).Value = date.ToString("MMMM");
		}
	}
}
