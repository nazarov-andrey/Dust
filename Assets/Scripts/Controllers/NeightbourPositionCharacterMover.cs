using System;
using Dust.Models;
using Dust.Views;
using Zenject;
using UnityEngine;

namespace Dust.Controllers {
	public class DefaultCharacterMover : ICharacterMover
	{
		public class Factory : Factory<Character, Direction, DefaultCharacterMover>
		{
		}
		
		private Character character;
		private CharacterView characterView;
		private Position destination;
		private IPositionVerctor2Mapper positionVerctor2Mapper;

		public DefaultCharacterMover (
			Character character,
			Direction direction,
			ICharacterViewResolver characterViewResolver,
			IPositionVerctor2Mapper positionVerctor2Mapper)
		{
			this.character = character;
			this.characterView = characterViewResolver.Resolve (character);
			this.destination = character.Position.Offset (direction);
			this.positionVerctor2Mapper = positionVerctor2Mapper;
		}

		private void CharacterViewMoved (object sender, EventArgs e)
		{
			character.Position = destination;
			characterView.Moved -= CharacterViewMoved;	
		}

		public void Run ()
		{
			Vector2 screenPosition = positionVerctor2Mapper.Map (destination);
			float time = characterView.PlayMove ();

			characterView.Move (screenPosition, time);
			characterView.Moved += CharacterViewMoved;
		}

		public Position Destination {
			get {
				return destination;
			}
		}
	}
}
