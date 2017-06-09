
namespace Dust.Models {
	public class PredefinedField : Field
	{
		public PredefinedField ()
			: base (
				9, 9, new Position (5, 4),
				new Character (
					new Position (2, 3), 20, 5, "Hero"),
				new Character[] {},
				new Obstacle[] {})
		{
		}
	}
}