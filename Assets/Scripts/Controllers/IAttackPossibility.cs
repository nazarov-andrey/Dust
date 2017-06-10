using Dust.Models;

namespace Dust.Controllers {
	public interface IAttackPossibility
	{
		bool IsPossible (Character applier, Character appliee);
	}
}
