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
		private static int HandledDaysCount = 7;

		private static String DynamicSheetName = "Лист1";
		private static String StatisticSheetName = "Карма";

		public static TimeSpan StatisticHoldTime = TimeSpan.FromDays(100);

		static void Main(string[] args)
		{
			var filename = @"C:\Users\RomanUser\Google Drive\Скиф\Расписание.xlsx";

			var book = new XLWorkbook(filename);
			var sheet = book.Worksheets.Worksheet(DynamicSheetName);
			
			var date = GetDate(sheet, 1);

			CreatePatternCopyIfNotExist(book, "Шаблон", date);

			CopyNearestDayToStaticTable(book, DynamicSheetName, date.ToString("MMMM"));
			AddNearestDayToStatistic(book, DynamicSheetName, StatisticSheetName);
			DeleteOldStatistic(book, StatisticSheetName, date);

			ShiftDays(book, DynamicSheetName);
			ShiftDates(book, DynamicSheetName);

			HandleNewDay(book, DynamicSheetName, StatisticSheetName, HandledDaysCount);

			var str = sheet.Row(4).Cell(33).Value.ToString().Split(',').ToList()[0];

			Console.WriteLine(DateTime.Parse(str));

			book.Save();

		}

		private static void HandleNewDay(XLWorkbook book, string dynamicTableName, string statisticTableName, int handledDaysCount)
		{
			var dynamicSheet = book.Worksheets.Worksheet(dynamicTableName);
			var statisticSheet = book.Worksheets.Worksheet(statisticTableName);

			var cells = new List<Tuple<IXLCell, int>>();
			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
				cells.Add(Tuple.Create(dynamicSheet.Cell(i, FirstColumnNum + handledDaysCount - 1),
						  statisticSheet.Cell(i, 2).Value.ToString().Split(',').Length));

			var r = new Random();
			cells = cells.OrderBy(x => (r.Next())).ToList();

			cells = cells.OrderBy(cell => cell.Item2).ToList();

			var first = cells.Where(cell => cell.Item1.Style.Fill.BackgroundColor == XLColor.Lime).ToList();
			var second = cells.Where(cell => cell.Item1.Style.Fill.BackgroundColor != XLColor.Lime && 
											 cell.Item1.Style.Fill.BackgroundColor != XLColor.Red).ToList();

			var pretendents = first.Concat(second).Take(2);
			foreach (var pretendent in pretendents)
			{
				pretendent.Item1.Value = "4";
			}

		}

		private static void DeleteOldStatistic(XLWorkbook book, string statisticTableName, DateTime currentDate)
		{
			var statisticSheet = book.Worksheets.Worksheet(statisticTableName);

			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
			{
				if (statisticSheet.Cell(i, 2).Value.ToString().Equals(""))
					continue;
				var dates = statisticSheet.Cell(i, 2).Value.ToString()
					.Split(',')
					.Select(s => DateTime.Parse(s))
					.Where(date => currentDate - date < StatisticHoldTime);

				statisticSheet.Cell(i, 2).Value = String.Join(",", dates);
			}

		}

		private static void AddNearestDayToStatistic(XLWorkbook book, string dynamicTableName, string statisticTableName)
		{
			var dynamicSheet = book.Worksheets.Worksheet(dynamicTableName);
			var statisticSheet = book.Worksheets.Worksheet(statisticTableName);

			var currentDate = GetDate(dynamicSheet, 1);

			for (int i = FirstStringNum; i < FirstStringNum + WorkersCount; i++)
			{
				if (dynamicSheet.Cell(i, FirstColumnNum).Value.ToString().Equals("4"))
				{
					if (!statisticSheet.Cell(i, 2).Value.ToString().Equals(""))
						statisticSheet.Cell(i, 2).Value += "," + currentDate.ToString("d");
					else
						statisticSheet.Cell(i, 2).Value += currentDate.ToString("d");
					
				}
			}
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
