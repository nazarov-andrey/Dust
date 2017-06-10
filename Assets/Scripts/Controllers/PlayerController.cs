using TouchScript.Gestures;
using Zenject;
using UnityEngine;
using Dust.Models;

namespace Dust.Controllers
{
	public class PlayerController : IInitializable
	{
		private FlickGesture flick;
		private PlayerTurnSignal playerTurnSignal;
		private WaitForPlayerTurnSignal waitForPlayerTurnSignal;
		private Character player;
		private Field field;
		private MoveTurnAction.PositionFactory moveTurnActionFactory;
		private float flickError;

		private PlayerController (
			FlickGesture flick,
			PlayerTurnSignal playerTurnSignal,
			WaitForPlayerTurnSignal waitForPlayerTurnSignal,
			Field field,
			MoveTurnAction.PositionFactory moveTurnActionFactory,
			float flickError)
		{
			this.flick = flick;
			this.playerTurnSignal = playerTurnSignal;
			this.waitForPlayerTurnSignal = waitForPlayerTurnSignal;
			this.moveTurnActionFactory = moveTurnActionFactory;
			this.player = field.Player;
			this.field = field;
			this.flickError = flickError;
		}

		private void FireSignalfNeeded (Direction direction)
		{
			Position position = player.Position.Offset (direction);	
			if (!field.IsPositionValid (position))
				return;

			flick.Flicked -= FlickFlicked;
			playerTurnSignal.Fire (
				moveTurnActionFactory.Create (player, position));
		}

		private bool IsAngleDiffAcceptable  (float angleA, float angleB)
		{
			float diff = Mathf.Abs (Mathf.DeltaAngle (angleA, angleB));
			return diff <= flickError;
		}

		private bool FireSignalIfAngleDiffAcceptable (
			float flickAngle, float referenceAngle, Direction direction)
		{
			bool result = IsAngleDiffAcceptable (referenceAngle, flickAngle);
			if (result)
				FireSignalfNeeded (direction);

			return result;
		}

		private void FlickFlicked (object sender, System.EventArgs e)
		{
			Vector2 vector = flick.ScreenFlickVector;
			float flickAngle = Mathf.Atan2 (vector.y, vector.x) * Mathf.Rad2Deg;

			if (FireSignalIfAngleDiffAcceptable (flickAngle, 0f, Direction.Right))
				return;

			if (FireSignalIfAngleDiffAcceptable (flickAngle, 90f, Direction.Up))
				return;

			if (FireSignalIfAngleDiffAcceptable (flickAngle, 180f, Direction.Left))
				return;

			if (FireSignalIfAngleDiffAcceptable (flickAngle, 270f, Direction.Down))
				return;
		}

		private void WaitForPlayerTurnSignalListener ()
		{
			flick.Flicked += FlickFlicked;
		}

		public void Initialize ()
		{
			waitForPlayerTurnSignal.Listen (WaitForPlayerTurnSignalListener);
		}
	}
}
