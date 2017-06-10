using Dust.Models;
using Dust.Views;
using System;

namespace Dust.Controllers {
	public interface ITurnAction
	{
		void Perform ();
		Position TargetPosition { get; }

		event EventHandler<EventArgs> Complete;
	}
}
