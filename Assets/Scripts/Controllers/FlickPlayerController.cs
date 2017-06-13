using TouchScript.Gestures;
using Zenject;
using UnityEngine;
using Dust.Models;

namespace Dust.Controllers {
	public class FlickPlayerController : IInitializable
	{
		private FlickGesture verticalFlickGesture;
		private FlickGesture horizontalFlickGesture;
		private PlayerTurnSignal playerTurnSignal;
		private WaitForPlayerTurnSignal waitForPlayerTurnSignal;
		private Character player;
		private Field field;
		private MoveTurnAction.PositionFactory moveTurnActionFactory;
//		private float flickError;

		private FlickPlayerController (
			[Inject (Id = "Vertical")] FlickGesture verticalFlickGesture,
			[Inject (Id = "Horizontal")] FlickGesture horizontalFlickGesture,
			PlayerTurnSignal playerTurnSignal,
			WaitForPlayerTurnSignal waitForPlayerTurnSignal,
			Field field,
			MoveTurnAction.PositionFactory moveTurnActionFactory)
		{
			this.verticalFlickGesture = verticalFlickGesture;
			this.horizontalFlickGesture = horizontalFlickGesture;
			this.playerTurnSignal = playerTurnSignal;
			this.waitForPlayerTurnSignal = waitForPlayerTurnSignal;
			this.moveTurnActionFactory = moveTurnActionFactory;
			this.player = field.Player;
			this.field = field;
//			this.flickError = flickError;
		}

		private void FireSignalfNeeded (Direction direction)
		{
			Position position = player.Position.Offset (direction);	
			if (!field.IsPositionValid (position))
				return;

			verticalFlickGesture.Flicked -= VerticalFlickGestureFlicked;
			horizontalFlickGesture.Flicked -= HorizontalFlickGestureFlicked;
			playerTurnSignal.Fire (
				moveTurnActionFactory.Create (player, position));
		}

//		private bool IsAngleDiffAcceptable  (float angleA, float angleB)
//		{
//			float diff = Mathf.Abs (Mathf.DeltaAngle (angleA, angleB));
//			return diff <= flickError;
//		}
//
//		private bool FireSignalIfAngleDiffAcceptable (
//			float flickAngle, float referenceAngle, Direction direction)
//		{
//			bool result = IsAngleDiffAcceptable (referenceAngle, flickAngle);
//			if (result)
//				FireSignalfNeeded (direction);
//
//			return result;
//		}

//		private void FlickFlicked (object sender, System.EventArgs e)
//		{
//			Vector2 vector = verticalFlickGesture.ScreenFlickVector;
//			float flickAngle = Mathf.Atan2 (vector.y, vector.x) * Mathf.Rad2Deg;
//
//			if (FireSignalIfAngleDiffAcceptable (flickAngle, 0f, Direction.Right))
//				return;
//
//			if (FireSignalIfAngleDiffAcceptable (flickAngle, 90f, Direction.Up))
//				return;
//
//			if (FireSignalIfAngleDiffAcceptable (flickAngle, 180f, Direction.Left))
//				return;
//
//			if (FireSignalIfAngleDiffAcceptable (flickAngle, 270f, Direction.Down))
//				return;
//		}

		private void WaitForPlayerTurnSignalListener ()
		{
			verticalFlickGesture.Flicked += VerticalFlickGestureFlicked;
			horizontalFlickGesture.Flicked += HorizontalFlickGestureFlicked;
		}

		private void HorizontalFlickGestureFlicked (object sender, System.EventArgs e)
		{
			if (horizontalFlickGesture.ScreenFlickVector.x > 0)
				FireSignalfNeeded (Direction.Right);
			else
				FireSignalfNeeded (Direction.Left);
		}

		private void VerticalFlickGestureFlicked (object sender, System.EventArgs e)
		{
			if (verticalFlickGesture.ScreenFlickVector.y > 0)
				FireSignalfNeeded (Direction.Up);
			else
				FireSignalfNeeded (Direction.Down);			
		}

		public void Initialize ()
		{
			waitForPlayerTurnSignal.Listen (WaitForPlayerTurnSignalListener);
		}
	}
}
