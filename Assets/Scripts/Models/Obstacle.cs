
namespace Dust.Models {
	public class Obstacle : PositionHolder
	{
		private string kind;

		public Obstacle (Position position, string kind) : base (position)
		{
			this.kind = kind;
		}

		public string Kind {
			get {
				return this.kind;
			}
		}
	}
}
