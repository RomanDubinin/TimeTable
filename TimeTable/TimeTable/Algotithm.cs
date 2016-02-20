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
		public List<WorkTable> GeneratedTables;

		public Algotithm()
		{
			GeneratedTables = new List<WorkTable>();
		}

		public void RecursiveAlgo(WorkTable workTable, WishTable wishTable, int maxAttemptNum)
		{
			if (CurrentTimeTableNum == maxAttemptNum)
				return;

			if (workTable.IsFilled(NecessaryCountOfWorkers))
			{
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
			var unfilledDays = workTable.GetUnfilledDayNumbers(NecessaryCountOfWorkers);

			foreach (var workerNum in sortedWorkerIndexes)
			{
				var preferredDays = wishTable.GetHisPreferredDays(workerNum);
				var possibleDays = preferredDays.Where(dayNum => workTable.DayIsFilled(dayNum, NecessaryCountOfWorkers)).Concat(unfilledDays);
				foreach (var dayNum in possibleDays)
				{
					if (ThisDayIsGoodToWork(wishTable, workTable, workerNum, dayNum))
					{
						var tableCopy = workTable.GetCopy();
						tableCopy[workerNum, dayNum] = WorkTableCell.Work;
						RecursiveAlgo(tableCopy, wishTable, maxAttemptNum);
					}
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