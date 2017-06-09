using UnityEngine;

namespace Dust.Views.Animations {
	public class AnimatorCharacterAnimation : ICharacterAnimation
	{
		private const string MoveTrigger = "Move";
		private const string AttackTrigger = "Attack";
		private const string HurtTrigger = "Hurt";
		private const string DieTrigger = "Die";

		private Animator animator;

		private AnimatorCharacterAnimation (Animator animator)
		{
			this.animator = animator;
		}

		public void Move ()
		{
			animator.SetTrigger (MoveTrigger);
		}

		public void Attack ()
		{
			animator.SetTrigger (AttackTrigger);
		}

		public void Hurt ()
		{
			animator.SetTrigger (HurtTrigger);
		}

		public void Die ()
		{
			animator.SetTrigger (DieTrigger);
		}
	}
}