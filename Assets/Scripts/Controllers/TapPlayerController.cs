using Zenject;
using TouchScript.Gestures;
using Dust.Views;
using Dust.Models;

namespace Dust.Controllers {
	public class TapPlayerController : IInitializable
	{
		private TapGesture tapGesture;
		private PlayerTurnSignal playerTurnSignal;
		private IPositionScreenPointMapper positionScreenPointMapper;
		private WaitForPlayerTurnSignal waitForPlayerTurnSignal;
		private Field field;
		private MoveTurnAction.PositionFactory moveTurnActionFactory;

		private TapPlayerController (
			TapGesture tapGesture,
			PlayerTurnSignal playerTurnSignal,
			IPositionScreenPointMapper positionScreenPointMapper,
			WaitForPlayerTurnSignal waitForPlayerTurnSignal,
			Field field,
			MoveTurnAction.PositionFactory moveTurnActionFactory)
		{
			this.tapGesture = tapGesture;
			this.playerTurnSignal = playerTurnSignal;
			this.positionScreenPointMapper = positionScreenPointMapper;
			this.waitForPlayerTurnSignal = waitForPlayerTurnSignal;
			this.field = field;
			this.moveTurnActionFactory = moveTurnActionFactory;
		}

		private void WaitForPlayerTurnSignalListener ()
		{
			tapGesture.Tapped += TapGestureTapped;
		}

		private void TapGestureTapped (object sender, System.EventArgs e)
		{
			Position position = positionScreenPointMapper.ScreenPointToPosition (
				tapGesture.ScreenPosition);

			Character player = field.Player;
			if (!field.IsPositionValid (position) || !player.Position.IsNeightbourWith (position))
				return;

			tapGesture.Tapped -= TapGestureTapped;
			playerTurnSignal.Fire (
				moveTurnActionFactory.Create (player, position));
		}

		public void Initialize ()
		{
			waitForPlayerTurnSignal.Listen (WaitForPlayerTurnSignalListener);
		}
	}
}
