using Zenject;
using Dust.Models;

namespace Dust.Controllers
{
	public class GameController : IInitializable
	{
		private Field field;
		private MoveCharacterSignal movePlayerSignal;

		private GameController (
			Field field,
			MoveCharacterSignal movePlayerSignal)
		{
			this.field = field;
			this.movePlayerSignal = movePlayerSignal;
		}

		private void MovePlayerSignalListener (ICharacterMover characterMover)
		{
			if (!field.IsPositionOccupied (characterMover.Destination))
				characterMover.Run ();
		}

		public void Initialize ()
		{
			movePlayerSignal.Listen (MovePlayerSignalListener);
		}


	}
}
