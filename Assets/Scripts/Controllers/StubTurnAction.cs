using Dust.Models;
using System;


namespace Dust.Controllers {
	public class StubTurnAction : ITurnAction
	{
		protected virtual void OnComplete (EventArgs e)
		{
			var handler = this.Complete;
			if (handler != null)
				handler (this, e);
		}

		public void Perform ()
		{
			OnComplete (EventArgs.Empty);
		}

		public Position TargetPosition {
			get {
				return Position.Zero;
			}
		}

		public event EventHandler<EventArgs> Complete;
	}
}
