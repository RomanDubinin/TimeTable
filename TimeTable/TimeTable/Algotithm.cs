using System.Collections.Generic;
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
					CurrentTimeTableNum++;
					return;
				}
				return;
			}

			var sortedWorkerIndexes = mainTable.GetOrderedByIncreaseOfWorkDays();
			foreach (var workerNum in sortedWorkerIndexes)
			{
				var preferredDays = mainTable
					.SelectDays(workerNum, WishTableCell.Yes);

				var secondStage = mainTable
					.SelectDays(workerNum, WishTableCell.Empty)
					.Where(dayNum => !mainTable.PreviousDayIsWork(workerNum, dayNum));

				var thirdStage = mainTable
					.SelectDays(workerNum, WishTableCell.Empty)
					.Where(dayNum => mainTable.PreviousDayIsWork(workerNum, dayNum));

				var possibleDays = preferredDays
					.Concat(secondStage)
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
	}
}