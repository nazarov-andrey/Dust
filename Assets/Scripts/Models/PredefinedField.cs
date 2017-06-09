
namespace Dust.Models {
	public class PredefinedField : Field
	{
		public PredefinedField ()
			: base (
				9, 9, new Position (2, -4),
				new Character (
					new Position (2, 3), 20, 5, "Hero"),
				new Character[] {
					new Character (
						new Position (0, 0), 10, 3, "Foe"),
					new Character (
						new Position (-1, -1), 5, 10, "Foe") },
				new Obstacle[] {
					new Obstacle (
						new Position (4, 0), "Obstacle A"),
					new Obstacle (
						new Position (-2, -3), "Obstacle D"),
					new Obstacle (
						new Position (2, 1), "Obstacle F") })
		{
		}
	}
}