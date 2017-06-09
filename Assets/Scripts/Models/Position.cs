
namespace Dust.Models {
	public class Position
	{
		private int row;
		private int col;

		public Position (int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		public int Row {
			get {
				return this.row;
			}
		}

		public int Col {
			get {
				return this.col;
			}
		}
	}
}