using Dust.Views;
using Dust.Models;
using Zenject;
using UnityEngine;

namespace Dust.Controllers {
	public class FieldController : IInitializable
	{
		private Field field;
		private IPositionVerctor2Mapper positionVerctor2Mapper;
		private CharacterView.Factory characterViewFactory;
		private ObstacleView.Factory obstacleViewFactory;
		private ExitView.Factory exitViewFactory;

		private FieldController (
			Field field,
			FieldView fieldView,
			IPositionVerctor2Mapper positionVerctor2Mapper,
			CharacterView.Factory characterViewFactory,
			ObstacleView.Factory obstacleViewFactory,
			ExitView.Factory exitViewFactory)
		{
			this.field = field;
			this.positionVerctor2Mapper = positionVerctor2Mapper;
			this.characterViewFactory = characterViewFactory;
			this.obstacleViewFactory = obstacleViewFactory;
			this.exitViewFactory = exitViewFactory;
		}

		private void PlaceOnGridView (OnGridView characterView, Position position)
		{
			Vector2 screenPosition = positionVerctor2Mapper.Map (position);
			characterView.Place (screenPosition);
		}

		private void CreateCharacterView (Character character)
		{
			CharacterView characterView = characterViewFactory.Create (character.Kind);
			PlaceOnGridView (characterView, character.Position);
		}

		private void CreateObstacleView (Obstacle obstacle)
		{
			ObstacleView obstacleView = obstacleViewFactory.Create (obstacle.Kind);
			PlaceOnGridView (obstacleView, obstacle.Position);
		}

		private void CreateExitView (Position position)
		{
			ExitView exitView = exitViewFactory.Create ();
			PlaceOnGridView (exitView, position);
		}

		public void Initialize ()
		{
			CreateCharacterView (field.Player);
			CreateExitView (field.Exit);

			foreach (var enemy in field.Enemies) {
				CreateCharacterView (enemy);
			}

			foreach (var obstacle in field.Obstacles) {
				CreateObstacleView (obstacle);
			}
		}
	}
}