namespace TimeTable
{
	public class MaiTableCell
	{
		public WorkTableCell WorkCell { get; set; }
		public WishTableCell WishCell { get; }

		public MaiTableCell(WorkTableCell workCell, WishTableCell wishCell)
		{
			WorkCell = workCell;
			WishCell = wishCell;
		}
	}
}