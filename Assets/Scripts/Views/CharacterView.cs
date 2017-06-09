using Dust.Views.Animations;
using Dust.Models;
using UnityEngine;
using Zenject;

namespace Dust.Views {
	public class CharacterView : OnGridView
	{
		public class Factory : Factory<string, CharacterView>
		{
		}

		private ICharacterAnimation characterAnimation;

		private CharacterView (
				RectTransform rectTransform,
				ICharacterAnimation characterAnimation)

			: base (rectTransform)
		{
			this.characterAnimation = characterAnimation;
		}

		public void Move (Vector2 position)
		{
			characterAnimation.Move ();	
		}

		public void Attack ()
		{
			characterAnimation.Attack ();
		}

		public void Hurt ()
		{
			characterAnimation.Hurt ();
		}

		public void Die ()
		{
			characterAnimation.Die ();	
		}


	}
}