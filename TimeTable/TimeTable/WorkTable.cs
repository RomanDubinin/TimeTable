using System;
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
		public List<List<WorkTableCell>> Matrix { get; }

		public WorkTable(List<List<WorkTableCell>> matrix)
		{
			Matrix = matrix;
		}

		public int CountOfWorkDays(int workerNum)
		{
			return Matrix[workerNum].Count(x => x == WorkTableCell.Work);
		}

		public bool Equals(WorkTable other)
		{
			for (int i = 0; i < Matrix.Count; i++)
			{
				for (int j = 0; j < Matrix[i].Count; j++)
				{
					if (Matrix[i][j] != other.Matrix[i][j])
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
	}
}
