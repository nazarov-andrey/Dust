using Dust.Views;
using Dust.Models;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

namespace Dust.Controllers {
	public class FieldController : ICharacterViewResolver, IInitializable
	{
		private Field field;
		private IPositionScreenPointMapper positionVerctor2Mapper;
		private CharacterView.Factory characterViewFactory;
		private ObstacleView.Factory obstacleViewFactory;
		private ExitView.Factory exitViewFactory;
		private Dictionary<PositionHolder, PositionHolderView> positionHolderViewMap;
		private RearrangeViewsSignal rearrangeViewsSignal;

		private FieldController (
			Field field,
			FieldView fieldView,
			IPositionScreenPointMapper positionVerctor2Mapper,
			CharacterView.Factory characterViewFactory,
			ObstacleView.Factory obstacleViewFactory,
			ExitView.Factory exitViewFactory,
			RearrangeViewsSignal rearrangeViewsSignal)
		{
			this.field = field;
			this.positionVerctor2Mapper = positionVerctor2Mapper;
			this.characterViewFactory = characterViewFactory;
			this.obstacleViewFactory = obstacleViewFactory;
			this.exitViewFactory = exitViewFactory;
			this.positionHolderViewMap = new Dictionary<PositionHolder, PositionHolderView> ();
			this.rearrangeViewsSignal = rearrangeViewsSignal;
		}

		private void TrackView (PositionHolder positionHolder, PositionHolderView positionHolderView)
		{
			positionHolderViewMap.Add (positionHolder, positionHolderView);
		}

		private void PlacePositionHolderView (PositionHolderView characterView, Position position)
		{
			Vector2 screenPosition = positionVerctor2Mapper.PositionToScreenPoint (position);
			characterView.Place (screenPosition);
		}

		private void CreateCharacterView (Character character)
		{
			CharacterView characterView = characterViewFactory.Create (character.Kind);
			PlacePositionHolderView (characterView, character.Position);
			TrackView (character, characterView);
		}

		private void CreateObstacleView (Obstacle obstacle)
		{
			ObstacleView obstacleView = obstacleViewFactory.Create (obstacle.Kind);
			PlacePositionHolderView (obstacleView, obstacle.Position);
			TrackView (obstacle, obstacleView);
		}

		private void CreateExitView (Exit exit)
		{
			ExitView exitView = exitViewFactory.Create ();
			PlacePositionHolderView (exitView, exit.Position);
			TrackView (exit, exitView);
		}

		private void RearrangeViews ()
		{
			List<PositionHolder> positionHolders = new List<PositionHolder> (
				field.PositionHolders);

			positionHolders.Sort (PositionHolder.CompareViewWeights);

			int i = 0;
			foreach (var positionHolder in positionHolders) {
				PositionHolderView positionHolderView = positionHolderViewMap [positionHolder];
				positionHolderView.SetSiblingIndex (i++);
			}			
		}

		private void RearrangeViewsSignalListener ()
		{
			RearrangeViews ();
		}

		public void Initialize ()
		{
			foreach (var enemy in field.Enemies) {
				CreateCharacterView (enemy);
			}

			foreach (var obstacle in field.Obstacles) {
				CreateObstacleView (obstacle);
			}

			CreateExitView (field.Exit);
			CreateCharacterView (field.Player);

			RearrangeViews ();
			rearrangeViewsSignal.Listen (RearrangeViewsSignalListener);
		}

		public CharacterView Resolve (Character character)
		{
			PositionHolderView characterView;
			if (!positionHolderViewMap.TryGetValue (character, out characterView))
				throw new System.ArgumentException ("Cannot find view for character " + character);

			return characterView as CharacterView;
		}
	}
}