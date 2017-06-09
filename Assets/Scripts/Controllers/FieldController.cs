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

		private FieldController (
			Field field,
			FieldView fieldView,
			IPositionVerctor2Mapper positionVerctor2Mapper,
			CharacterView.Factory characterViewFactory)
		{
			this.field = field;
			this.positionVerctor2Mapper = positionVerctor2Mapper;
			this.characterViewFactory = characterViewFactory;
		}

		private void PlaceCharacter (CharacterView characterView, Position position)
		{
			Vector2 screenPosition = positionVerctor2Mapper.Map (position);
			characterView.Place (screenPosition);
		}

		private void CreateCharacterView (Character character)
		{
			CharacterView characterView = characterViewFactory.Create (character.Kind);
			PlaceCharacter (characterView, character.Position);
		}

		public void Initialize ()
		{
			CreateCharacterView (field.Player);

			foreach (var enemy in field.Enemies) {
				CreateCharacterView (enemy);
			}
		}
	}
}