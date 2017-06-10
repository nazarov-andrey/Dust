using Dust.Models;

namespace Dust.Controllers {
	public class ExitReachedVictoryCondition : IVictoryCondition
	{
		private Field field;

		private ExitReachedVictoryCondition (Field field)
		{
			this.field = field;
		}

		public bool IsSatisfied ()
		{
			return field.Player.Position.Equals (field.Exit.Position);
		}
	}
}
