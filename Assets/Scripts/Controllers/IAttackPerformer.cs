using System;

namespace Dust.Controllers {
	public interface IAttackPerformer
	{
		void Perform ();
		event EventHandler<EventArgs> Complete;
	}
}
