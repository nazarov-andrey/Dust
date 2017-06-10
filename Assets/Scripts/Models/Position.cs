using UnityEngine;


namespace Dust.Models {
	public class Position
	{
		private int row;
		private int col;

		public Position (int col, int row)
		{
			this.col = col;
			this.row = row;
		}

		public Position Offset (Direction direction)
		{
			switch (direction) {
			case Direction.Down:
				return new Position (col, row - 1);

			case Direction.Up:
				return new Position (col, row + 1);

			case Direction.Left:
				return new Position (col - 1, row);

			case Direction.Right:
				return new Position (col + 1, row);

			default:
				throw new System.ArgumentException ("Unknown direction " + direction);
			}
		}

		public Direction GetDirectionTo (Position position)
		{
			int colDiff = this.col - position.col;
			int rowDiff = this.row - position.row;

			if (Mathf.Abs (colDiff) > Mathf.Abs (rowDiff))
				return colDiff > 0
					? Direction.Left
					: Direction.Right;
			else
				return rowDiff > 0
					? Direction.Up
					: Direction.Down;
		}

		public Position Add (Position position)
		{
			return new Position (col + position.col, row + position.row);
		}

		public Position Sub (Position position)
		{
			return new Position (col - position.col, row - position.row);
		}

		public bool IsNeightbourWith (Position position)
		{
			Position diff = this.Sub (position);
			return diff.Magnitude == 1f;
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Position))
				return false;
			Position other = (Position)obj;
			return Row == other.Row && Col == other.Col;
		}

		public override int GetHashCode ()
		{
			unchecked {
				return Row.GetHashCode () ^ Col.GetHashCode ();
			}
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

		public float Magnitude {
			get {
				return Mathf.Sqrt (Mathf.Pow (col, 2f) + Mathf.Pow (row, 2f));
			}
		}

		public static Position Zero {
			get {
				return new Position (0, 0);
			}
		}
	}
}