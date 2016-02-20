using System.Collections.Generic;

namespace TimeTable
{
	using NUnit.Framework;

	[TestFixture]
	class Tests
	{
		[Test]
		public void UnfilledDaysTest()
		{
			var innerTable = new List<List<WorkTableCell>>()
			{
				new List<WorkTableCell>() { WorkTableCell.Work,		WorkTableCell.Empty,	WorkTableCell.Work },
				new List<WorkTableCell>() { WorkTableCell.Empty,	WorkTableCell.Work,		WorkTableCell.Empty },
				new List<WorkTableCell>() { WorkTableCell.Work,		WorkTableCell.Empty,	WorkTableCell.Work },
				new List<WorkTableCell>() { WorkTableCell.Empty,	WorkTableCell.Empty,	WorkTableCell.Empty }
			};
			var table = new WorkTable(innerTable);

			var unfilledDays = table.GetUnfilledDayNumbers(2);
			Assert.AreEqual(1, unfilledDays.Count);
			Assert.AreEqual(1, unfilledDays[0]);
		}

		[Test]
		public void DeepCopyTest()
		{
			var innerTable = new List<List<WorkTableCell>>()
			{
				new List<WorkTableCell>() { WorkTableCell.Work,       WorkTableCell.Empty,    WorkTableCell.Work },
				new List<WorkTableCell>() { WorkTableCell.Empty,    WorkTableCell.Empty,    WorkTableCell.Empty },
				new List<WorkTableCell>() { WorkTableCell.Work,       WorkTableCell.Work,       WorkTableCell.Empty },
				new List<WorkTableCell>() { WorkTableCell.Empty,    WorkTableCell.Empty,    WorkTableCell.Empty }
			};
			var table = new WorkTable(innerTable);
			var copy = table.GetCopy();

			copy[0, 0] = WorkTableCell.Empty;

			Assert.AreEqual(WorkTableCell.Work, table[0, 0]);
			Assert.AreEqual(WorkTableCell.Empty, copy[0, 0]);
		}

		[Test]
		public void EqualsTest()
		{
			var innerTable = new List<List<WorkTableCell>>()
			{
				new List<WorkTableCell>() { WorkTableCell.Work,       WorkTableCell.Empty,    WorkTableCell.Work    },
				new List<WorkTableCell>() { WorkTableCell.Empty,    WorkTableCell.Empty,    WorkTableCell.Empty },
				new List<WorkTableCell>() { WorkTableCell.Work,       WorkTableCell.Work,       WorkTableCell.Empty },
				new List<WorkTableCell>() { WorkTableCell.Empty,    WorkTableCell.Empty,    WorkTableCell.Empty }
			};
			var table = new WorkTable(innerTable);
			var copy = table.GetCopy();

			Assert.IsTrue(table.Equals(copy));
		}

	}
}
