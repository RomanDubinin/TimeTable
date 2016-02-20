using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TimeTable
{
	class Program
	{

		static void Main(string[] args)
		{
			var wishMatrix = new List<List<WishTableCell>>()
			{
				//new List<WishTableCell>() { WishTableCell.No	,	WishTableCell.No,		WishTableCell.No,	WishTableCell.No,	WishTableCell.No},
				//new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Yes,		WishTableCell.Empty,	WishTableCell.Yes,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Yes,		WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Yes,	WishTableCell.Empty },
				//new List<WishTableCell>() { WishTableCell.Yes,		WishTableCell.Yes,		WishTableCell.Empty,	WishTableCell.Yes,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				new List<WishTableCell>() { WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty,	WishTableCell.Empty },
				
			};

			var wishTable = new WishTable(wishMatrix);
			var workTable = WorkTable.CreateEmpty(wishMatrix.Count, wishMatrix[0].Count);

			var alg = new Algotithm();
			alg.RecursiveAlgo(workTable, wishTable, 10);
		}

		

	}
}
