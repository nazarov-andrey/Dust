
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
	}
}
