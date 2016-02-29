using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable
{
	public class MainTable
	{
		private List<List<MaiTableCell>> Table;
		private int WorkersToOneDay;

		public MainTable(List<List<MaiTableCell>> table, int workersToOneDay)
		{
			Table = table;
			WorkersToOneDay = workersToOneDay;
		}

		public MainTable(List<List<WorkTableCell>> workTable, List<List<WishTableCell>> wishTable, int workersToOneDay)
		{
			WorkersToOneDay = workersToOneDay;
			Table = new List<List<MaiTableCell>>();
			for (int i = 0; i < workTable.Count; i++)
			{
				Table.Add(new List<MaiTableCell>());
				for (int j = 0; j < workTable[i].Count; j++)
				{
					Table[i].Add(new MaiTableCell(workTable[i][j], wishTable[i][j]));
				}
			}
		}

		public WorkTable GetWorkTable
		{
			get
			{
				var workTable = new List<List<WorkTableCell>>();
				for (int i = 0; i < Table.Count; i++)
				{
					workTable.Add(new List<WorkTableCell>());
					for (int j = 0; j < Table[i].Count; j++)
					{
						workTable[i].Add(Table[i][j].WorkCell);
					}
				}
				return new WorkTable(workTable);
			}
		}

		public WishTable GetWishTable
		{
			get
			{
				var wishTable = new List<List<WishTableCell>>();
				for (int i = 0; i < Table.Count; i++)
				{
					wishTable.Add(new List<WishTableCell>());
					for (int j = 0; j < Table[i].Count; j++)
					{
						wishTable[i].Add(Table[i][j].WishCell);
					}
				}
				return new WishTable(wishTable);
			}
		}

		public static MainTable FromTtwoTables(WorkTable workTable, WishTable wishTable, int workersToOneDay)
		{
			return new MainTable(workTable.Matrix, wishTable.Matrix, workersToOneDay);
		}

		public List<int> GetOrderedByIncreaseOfWorkDays()
		{
			var now = DateTime.Now;
			var r = new Random(now.Hour + now.Second + now.Millisecond);
			var orderedrows = Table
				.OrderBy(row => row.Count(x => x.WishCell == WishTableCell.Yes && x.WorkCell == WorkTableCell.Empty) == 0? 1 : 0)
				.ThenBy(row => row.Count(x => x.WishCell == WishTableCell.Yes && x.WorkCell == WorkTableCell.Empty))
				.ThenBy(row => row.Count(x => x.WorkCell == WorkTableCell.Work))
				.ThenBy(x => r.Next(DateTime.Now.Second))
				.ToList();
			var indexes = orderedrows.Select(row => Table.IndexOf(row));
			return indexes.ToList();
		}

		public void SetWork(int workerNum, int dayNum)
		{
			Table[workerNum][dayNum].WorkCell = WorkTableCell.Work;
			
				if (DayIsFilled(dayNum))
				{
					for (int i = 0; i < Table.Count; i++)
					{
						Table[i][dayNum].WishCell = WishTableCell.No;
					}
				}
		}

		public bool AllDaysAreFilled()
		{
			if (GetUnfilledDayNumbers().Count == 0)
				return true;
			return false;
		}

		public List<int> GetUnfilledDayNumbers()
		{
			List<int> result = new List<int>();
			int n = Table[0].Count;
			for (int dayNumber = 0; dayNumber < n; dayNumber++)
			{
				if (!DayIsFilled(dayNumber))
					result.Add(dayNumber);
			}
			return result;
		}

		public bool DayIsFilled(int dayNum)
		{
			var countOfWorkersToday = 0;
			foreach (var row in Table)
			{
				if (row[dayNum].WorkCell == WorkTableCell.Work)
					countOfWorkersToday++;
			}
			if (countOfWorkersToday < WorkersToOneDay)
				return false;
			return true;
		}

		public List<int> SelectDays(int workerNum, WishTableCell cellValue)
		{
			List<int> result = new List<int>();
			int n = Table[workerNum].Count;
			for (int dayNumber = 0; dayNumber < n; dayNumber++)
			{
				if (Table[workerNum][dayNumber].WishCell == cellValue)
					result.Add(dayNumber);
			}
			return result;
		}

		public List<int> SelectDays(int workerNum, WorkTableCell cellValue)
		{
			List<int> result = new List<int>();
			int n = Table[workerNum].Count;
			for (int dayNumber = 0; dayNumber < n; dayNumber++)
			{
				if (Table[workerNum][dayNumber].WorkCell == cellValue)
					result.Add(dayNumber);
			}
			return result;
		}

		public MaiTableCell this[int i, int j]
		{
			get { return Table[i][j]; }
			set { Table[i][j] = value; }
		}

		public bool PreviousDayIsWork(int workerNum, int dayNum)
		{
			if (dayNum == 0 || Table[workerNum][dayNum-1].WorkCell == WorkTableCell.Empty)
				return false;

			return true;
		}

		public MainTable GetCopy()
		{
			var copy = new List<List<MaiTableCell>>();
			for (int i = 0; i < Table.Count; i++)
			{
				copy.Add(new List<MaiTableCell>());
				for (int j = 0; j < Table[i].Count; j++)
				{
					copy[i].Add(Table[i][j]);
				}

			}
			return new MainTable(copy, WorkersToOneDay);
		}
	}
}