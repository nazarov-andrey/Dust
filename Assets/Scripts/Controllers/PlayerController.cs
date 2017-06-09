using TouchScript.Gestures;
using Zenject;
using UnityEngine;
using Dust.Models;

namespace Dust.Controllers
{
	public class PlayerController : IInitializable
	{
		private FlickGesture horizontalFlick;
		private FlickGesture verticalFlick;
		private MoveCharacterSignal movePlayerSignal;
		private Character player;
		private DefaultCharacterMover.Factory defaultCharacterMoverFactory;

		private PlayerController (
			[Inject (Id = "Horizontal")] FlickGesture horizontalFlick,
			[Inject (Id = "Vertical")] FlickGesture verticalFlick,
			MoveCharacterSignal movePlayerSignal,
			Character player,
			DefaultCharacterMover.Factory defaultCharacterMoverFactory)
		{
			this.horizontalFlick = horizontalFlick;
			this.verticalFlick = verticalFlick;
			this.movePlayerSignal = movePlayerSignal;
			this.defaultCharacterMoverFactory = defaultCharacterMoverFactory;
			this.player = player;
		}

		private void FireSignal (Direction direction)
		{
			movePlayerSignal.Fire (
				defaultCharacterMoverFactory.Create (player, direction));
		}

		private void VerticalFlickFlicked (object sender, System.EventArgs e)
		{
			Direction direction = Direction.Up;
			if (verticalFlick.ScreenFlickVector.y < 0)
				direction = Direction.Down;

			FireSignal (direction);
		}

		private void HorizontalFlickFlicked (object sender, System.EventArgs e)
		{
			Direction direction = Direction.Right;
			if (horizontalFlick.ScreenFlickVector.x < 0)
				direction = Direction.Left;

			FireSignal (direction);
		}

		public void Initialize ()
		{
			horizontalFlick.Flicked += HorizontalFlickFlicked;
			verticalFlick.Flicked += VerticalFlickFlicked;
		}
	}
}
