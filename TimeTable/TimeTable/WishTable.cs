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
		public List<List<WishTableCell>> Matrix { get; }

		public WishTable(List<List<WishTableCell>> matrix)
		{
			Matrix = matrix;
		}
	}
}