using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TimeTable
{
	class Program
	{
		static public int Count = 2;
		static public int CurrentTimeTableNum = 1;

		static public List<WorkTable> GeneratedTables; 

		static void Main(string[] args)
		{
			var wishMatrix = new List<List<WishTableCell>>()
			{
				new List<WishTableCell>() { WishTableCell.No	,	WishTableCell.No,		WishTableCell.No,	WishTableCell.No,	WishTableCell.No},
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Yes,		WishTableCell.Empty,	WishTableCell.Yes,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Yes,		WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Yes,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Yes,		WishTableCell.Yes,		WishTableCell.Empty,	WishTableCell.Yes,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				
			};

			var wishTable = new WishTable(wishMatrix);
			var workTable = WorkTable.CreateEmpty(wishMatrix.Count, wishMatrix[0].Count);
			GeneratedTables = new List<WorkTable>();

			RecursiveAlgo(workTable, wishTable);
			Console.WriteLine(CurrentTimeTableNum);
		}

		public static void RecursiveAlgo(WorkTable workTable, WishTable wishTable)
		{
			if (workTable.IsFilled(Count))
			{
				if (CurrentTimeTableNum % 10 == 0)
				{
					Console.WriteLine(CurrentTimeTableNum);
				}
				if (!GeneratedTables.Contains(workTable))
				{
					GeneratedTables.Add(workTable);
					WriteTimeTableToFile(workTable, wishTable);
					CurrentTimeTableNum++;
					return;
				}
				return;
			}

			var sortedWorkerIndexes = workTable.GetOrderedByIncreaseOfWorkDays();
			var unfilledDays = workTable.GetUnfilledDayNumbers(Count);

			foreach (var workerNum in sortedWorkerIndexes)
			{
				var preferredDays = wishTable.GetHisPreferredDays(workerNum);
				var possibleDays = preferredDays.Where(dayNum => workTable.DayIsFilled(dayNum, Count)).Concat(unfilledDays);
				foreach (var dayNum in possibleDays)
				{
					if (ThisDayIsGoodToWork(wishTable, workTable, workerNum, dayNum))
					{
						var tableCopy = workTable.GetCopy();
						tableCopy[workerNum, dayNum] = WorkTableCell.Work;
						RecursiveAlgo(tableCopy, wishTable);
					}
				}
			}
			
		}

		public static bool ThisDayIsGoodToWork(WishTable wishTable, WorkTable workTable, int workerNum, int dayNum)
		{
			if (
				(wishTable[workerNum, dayNum] == WishTableCell.Empty ||
				 wishTable[workerNum, dayNum] == WishTableCell.Yes) &&
				workTable[workerNum, dayNum] == WorkTableCell.Empty)
				return true;
			return false;


		}

		public static void WriteTimeTableToFile(WorkTable workTtable, WishTable wishTable)
		{
			using (StreamWriter writer = new StreamWriter(CurrentTimeTableNum + ".txt"))
			{
				for (int i = 0; i < workTtable.WorkersCount; i++)
				{
					writer.Write(i + "|");
					for (int j = 0; j < workTtable.DaysCount; j++)
					{
						if (workTtable[i, j] == WorkTableCell.Work)
							writer.Write("#");
						else if (wishTable[i, j] == WishTableCell.No)
							writer.Write("-");
						else if (wishTable[i, j] == WishTableCell.Yes && workTtable[i, j] != WorkTableCell.Work)
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
