using Dust.Views.Animations;
using Dust.Models;
using UnityEngine;
using Zenject;

namespace Dust.Views {
	public class CharacterView
	{
		public class Factory : Factory<Character>
		{
		}

		private ICharacterAnimation characterAnimation;

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