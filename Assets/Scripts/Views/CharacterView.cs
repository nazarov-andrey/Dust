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

		private CharacterView (ICharacterAnimation characterAnimation)
		{
			this.characterAnimation = characterAnimation;
		}

		public float PlayAttack ()
		{
			return characterAnimation.Attack ();
		}

		public float PlayHurt ()
		{
			return characterAnimation.Hurt ();
		}

		public float PlayDie ()
		{
			return characterAnimation.Die ();	
		}

		public float PlayMove ()
		{
			return characterAnimation.Move ();
		}
	}
}