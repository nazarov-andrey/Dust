using Dust.Views.Animations;
using Dust.Models;
using UnityEngine;
using Zenject;

namespace Dust.Views {
	public class CharacterView
	{
		public class Factory : Factory<string, CharacterView>
		{
		}

		private ICharacterAnimation characterAnimation;
		private RectTransform rectTransform;

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

		public void Place (Vector2 position)
		{
			rectTransform.anchoredPosition = position;
		}
	}
}