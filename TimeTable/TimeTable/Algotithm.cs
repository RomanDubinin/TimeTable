using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TimeTable
{
	public class Algotithm
	{
		private int CurrentTimeTableNum = 0;
		private int NecessaryCountOfWorkers = 2;
		public List<MainTable> GeneratedTables { get; private set; }

		public Algotithm()
		{
			GeneratedTables = new List<MainTable>();
		}

		public void RecursiveAlgo(MainTable mainTable, int maxAttemptNum)
		{
			if (CurrentTimeTableNum == maxAttemptNum)
				return;

			if (mainTable.AllDaysAreFilled(NecessaryCountOfWorkers))
			{
				if (!GeneratedTables.Contains(mainTable))
				{
					GeneratedTables.Add(mainTable);
					WriteTimeTableToFile(mainTable.GetWorkTable, mainTable.GetWishTable);
					CurrentTimeTableNum++;
					return;
				}
				return;
			}

			var sortedWorkerIndexes = mainTable.GetOrderedByIncreaseOfWorkDays();
			foreach (var workerNum in sortedWorkerIndexes)
			{
				var preferredDays = mainTable.SelectDays(workerNum, WishTableCell.Yes);

				var seconfStage = mainTable.SelectDays(workerNum, WishTableCell.Empty)
					.Where(dayNum => !mainTable.PreviousDayIsWork(workerNum, dayNum));

				var thirdStage = mainTable.SelectDays(workerNum, WishTableCell.Empty)
					.Where(dayNum => mainTable.PreviousDayIsWork(workerNum, dayNum));

				var possibleDays = preferredDays
								   .Concat(seconfStage)
								   .Concat(thirdStage)
								   .Where(dayNum => !mainTable.DayIsFilled(dayNum, NecessaryCountOfWorkers))
								   .Where(dayNum => mainTable[workerNum, dayNum].WorkCell != WorkTableCell.Work);

				foreach (var dayNum in possibleDays)
				{
					var tableCopy = mainTable.GetCopy();
					tableCopy[workerNum, dayNum].WorkCell = WorkTableCell.Work;
					RecursiveAlgo(mainTable, maxAttemptNum);
				}
			}

		}

		public bool ThisDayIsGoodToWork(WishTable wishTable, WorkTable workTable, int workerNum, int dayNum)
		{
			if (
				(wishTable[workerNum, dayNum] == WishTableCell.Empty ||
				 wishTable[workerNum, dayNum] == WishTableCell.Yes) &&
				workTable[workerNum, dayNum] == WorkTableCell.Empty)
				return true;
			return false;


		}

		public void WriteTimeTableToFile(WorkTable workTtable, WishTable wishTable)
		{
			using (StreamWriter writer = new StreamWriter(CurrentTimeTableNum + ".txt"))
			{
				for (int i = 0; i < workTtable.WorkersCount; i++)
				{
					writer.Write("{0, 2}|", i);
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