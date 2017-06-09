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

		private float GetCurrentStateDuration ()
		{
			AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo (0);
			return animatorStateInfo.length;
		}

		private float SetTriggerAndReturnDuration (string trigger)
		{
			animator.SetTrigger (trigger);
			return GetCurrentStateDuration ();
		}

		public float Move ()
		{
			return SetTriggerAndReturnDuration (MoveTrigger);
		}

		public float Attack ()
		{
			return SetTriggerAndReturnDuration (AttackTrigger);
		}

		public float Hurt ()
		{
			return SetTriggerAndReturnDuration (HurtTrigger);
		}

		public float Die ()
		{
			return SetTriggerAndReturnDuration (DieTrigger);
		}
	}
}