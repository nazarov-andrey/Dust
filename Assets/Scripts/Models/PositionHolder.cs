using System.Collections.Generic;


namespace Dust.Models {
	public abstract class PositionHolder
	{
		private Position position;

		protected PositionHolder (Position position)
		{
			this.position = position;
		}

		public Position Position {
			get {
				return this.position;
			}
			set {
				position = value;
			}
		}

		public virtual int ViewWeight {
			get {
				return -Position.Row * 100 + Position.Col;
			}
		}

		public virtual bool IsActive {
			get {
				return true;
			}
		}

		public static int CompareViewWeights (PositionHolder a, PositionHolder b)
		{
			return a.ViewWeight.CompareTo (b.ViewWeight);
		}
	}
}
