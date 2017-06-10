using System;
using Dust.Models;
using Dust.Views;
using Zenject;
using UnityEngine;

namespace Dust.Controllers {
	public class MoveTurnAction : ITurnAction
	{
		public class DirectionFactory : Factory<Character, Direction, MoveTurnAction>
		{
		}

		public class PositionFactory : Factory<Character, Position, MoveTurnAction>
		{
		}
		
		private Character character;
		private CharacterView characterView;
		private Position targetPosition;
		private Direction direction;
		private IPositionVerctor2Mapper positionVerctor2Mapper;
		private RearrangeViewsSignal rearrangeViewsSignal;

		private MoveTurnAction (
			Character character,
			[InjectOptional] Direction direction,
			[InjectOptional] Position targetPosition,
			ICharacterViewResolver characterViewResolver,
			IPositionVerctor2Mapper positionVerctor2Mapper,
			RearrangeViewsSignal rearrangeViewsSignal)
		{
			this.character = character;
			this.characterView = characterViewResolver.Resolve (character);
			this.positionVerctor2Mapper = positionVerctor2Mapper;
			this.rearrangeViewsSignal = rearrangeViewsSignal;

			if (targetPosition != null) {
				this.targetPosition = targetPosition;
				this.direction = character.Position.GetDirectionTo (targetPosition);
			} else {
				this.direction = direction;
				this.targetPosition = character.Position.Offset (direction);
			}
		}

		private void CharacterViewMoved (object sender, EventArgs e)
		{
			character.Position = targetPosition;
			characterView.Moved -= CharacterViewMoved;
			rearrangeViewsSignal.Fire ();
			OnComplete (EventArgs.Empty);
		}

		public void Perform ()
		{
			Vector2 screenPosition = positionVerctor2Mapper.Map (targetPosition);
			float time = characterView.PlayMove ();

			characterView.Look (direction);
			characterView.Move (screenPosition, time);
			characterView.Moved += CharacterViewMoved;
		}

		protected virtual void OnComplete (EventArgs e)
		{
			var handler = this.Complete;
			if (handler != null)
				handler (this, e);
		}

		public Position TargetPosition {
			get {
				return targetPosition;
			}
		}

		public event EventHandler<EventArgs> Complete;
	}
}
