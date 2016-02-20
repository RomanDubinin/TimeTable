﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeTable
{
	public enum WorkTableCell
	{
		Empty,
		Work
	}

	public class WorkTable : IEquatable<WorkTable>
	{
		private List<List<WorkTableCell>> Matrix;

		public WorkTable(List<List<WorkTableCell>> matrix)
		{
			Matrix = matrix;
		}

		public int DaysCount
		{
			get {return Matrix[0].Count;}
		}

		public int WorkersCount
		{
			get { return Matrix.Count; }
		}

		public int RowsCount
		{
			get { return Matrix.Count; }
		}

		public int ColumnsCount
		{
			get { return Matrix[0].Count; }
		}

		public WorkTableCell this[int i, int j]
		{
			get { return Matrix[i][j]; }
			set { Matrix[i][j] = value; }
		}

		public int CountOfWorkDays(List<WorkTableCell> row)
		{
			return row.Count(x => x == WorkTableCell.Work);
		}

		public int CountOfWorkDays(int workerNum)
		{
			return Matrix[workerNum].Count(x => x == WorkTableCell.Work);
		}

		public bool DayIsFilled(int dayNum, int countOfWorkersToFill)
		{
			var countOfWorkersToday = 0;
			foreach (var row in Matrix)
			{
				if (row[dayNum] == WorkTableCell.Work)
					countOfWorkersToday++;
			}
			if (countOfWorkersToday < countOfWorkersToFill)
				return false;
			return true;
		}

		public List<int> GetUnfilledDayNumbers(int countOfWorkersToFill)
		{
			List<int> result = new List<int>();
			int n = Matrix[0].Count;
			for (int i = 0; i < n; i++)
			{
				if (!DayIsFilled(i, countOfWorkersToFill))
					result.Add(i);
			}
			return result;
		}

		public bool IsFilled(int countOfWorkersToFill)
		{
			if (GetUnfilledDayNumbers(countOfWorkersToFill).Count == 0)
				return true;
			return false;
		}

		public List<int> GetOrderedByIncreaseOfWorkDays()
		{
			var orderedrows = Matrix.OrderBy(row => CountOfWorkDays(row)).ToList();
			var indexes = orderedrows.Select(row => Matrix.IndexOf(row));
			return indexes.ToList();
		}

		public WorkTable GetCopy()
		{
			var copy = new List<List<WorkTableCell>>();
			for (int i = 0; i < Matrix.Count; i++)
			{
				copy.Add(new List<WorkTableCell>());
				for (int j = 0; j < Matrix[i].Count; j++)
				{
					copy[i].Add(Matrix[i][j]);
				}
				
			}
			return new WorkTable(copy);
		}

		public bool Equals(WorkTable other)
		{
			for (int i = 0; i < Matrix.Count; i++)
			{
				for (int j = 0; j < Matrix[i].Count; j++)
				{
					if (this[i, j] != other[i, j])
						return false;
				}
			}
			return true;
		}


		public static WorkTable CreateEmpty(int workersNum, int daysNum)
		{
			var workMatrix = new List<List<WorkTableCell>>();
			for (int i = 0; i < workersNum; i++)
			{
				workMatrix.Add(new List<WorkTableCell>(daysNum));
				for (int j = 0; j < daysNum; j++)
				{
					workMatrix[i].Add(WorkTableCell.Empty);
				}
			}
			return new WorkTable(workMatrix);
		}

		public bool HaveLeftFilledNeighbour(int workerNum, int dayNum)
		{
			if (dayNum == 0 || Matrix[workerNum][dayNum] == WorkTableCell.Empty)
				return false;

			return true;
		}
	}
}
