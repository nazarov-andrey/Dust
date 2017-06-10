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
		private WaitForEnemyTurnSignal waitForEnemyTurnSignal;
		private WaitForPlayerTurnSignal waitForPlayerTurnSignal;
		private Character player;
		private MoveTurnAction.DirectionFactory defaultCharacterMoverFactory;
		private float flickError;

		private PlayerController (
			FlickGesture flick,
			PlayerTurnSignal playerTurnSignal,
			WaitForEnemyTurnSignal waitForEnemyTurnSignal,
			WaitForPlayerTurnSignal waitForPlayerTurnSignal,
			Character player,
			MoveTurnAction.DirectionFactory defaultCharacterMoverFactory,
			float flickError)
		{
			this.flick = flick;
			this.playerTurnSignal = playerTurnSignal;
			this.waitForEnemyTurnSignal = waitForEnemyTurnSignal;
			this.waitForPlayerTurnSignal = waitForPlayerTurnSignal;
			this.defaultCharacterMoverFactory = defaultCharacterMoverFactory;
			this.player = player;
			this.flickError = flickError;
		}

		private void FireSignal (Direction direction)
		{
			playerTurnSignal.Fire (
				defaultCharacterMoverFactory.Create (player, direction));
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
				FireSignal (direction);

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

		private void WaitForEnemyTurnSignalListener (Character character)
		{
			flick.Flicked -= FlickFlicked;
		}

		public void Initialize ()
		{
			waitForPlayerTurnSignal.Listen (WaitForPlayerTurnSignalListener);
			waitForEnemyTurnSignal.Listen (WaitForEnemyTurnSignalListener);
		}
	}
}
