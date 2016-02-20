using System.Collections.Generic;
using System.Linq;

namespace TimeTable
{
	public enum WishTableCell
	{
		Empty,
		Yes,
		No,
		OneOfTwo
	}

	public class WishTable
	{
		private List<List<WishTableCell>> Matrix;

		public WishTable(List<List<WishTableCell>> matrix)
		{
			Matrix = matrix;
		}

		public WishTableCell this[int i, int j]
		{
			get { return Matrix[i][j]; }
		}

		public List<int> GetHisPreferredDays(int workerNum)
		{
			List<int> result = new List<int>();
			int n = Matrix[workerNum].Count;
			for (int i = 0; i < n; i++)
			{
				if (Matrix[workerNum][i] == WishTableCell.Yes)
					result.Add(i);
			}
			return result;
		}
	}
}