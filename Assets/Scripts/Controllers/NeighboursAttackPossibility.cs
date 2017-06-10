using Dust.Models;

namespace Dust.Controllers {
	public class NeighboursAttackPossibility : IAttackPossibility
	{
		public bool IsPossible (Character attacker, Character attackee)
		{
			return attacker.Position.IsNeightbourWith (attackee.Position);
		}
	}
}
