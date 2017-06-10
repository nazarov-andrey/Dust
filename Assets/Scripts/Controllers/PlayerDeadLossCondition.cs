using Dust.Models;

namespace Dust.Controllers {
	public class PlayerDeadLossCondition : ILossCondtion
	{
		private Field field;

		private PlayerDeadLossCondition (Field field)
		{
			this.field = field;
		}

		public bool IsSatisfied ()
		{
			return !field.Player.IsAlive;
		}
	}
}
