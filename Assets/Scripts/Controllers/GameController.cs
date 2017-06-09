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
			Position destination = characterMover.Destination;
			if (!field.IsPositionValid (destination))
				return;

			PositionHolder positionHolder;
			if (!field.TryGetPositionHolder (characterMover.Destination, out positionHolder)) {
				characterMover.Run ();
				return;
			}
		}

		public void Initialize ()
		{
			movePlayerSignal.Listen (MovePlayerSignalListener);
		}


	}
}
