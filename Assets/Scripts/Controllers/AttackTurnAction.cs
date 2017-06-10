using Dust.Models;
using Dust.Views;
using Zenject;
using System;
using UnityEngine;

namespace Dust.Controllers {
	public class AttackTurnAction : ITickable, ITurnAction
	{
		public class Factory : Factory<Character, Character, AttackTurnAction>
		{
		}

		private Character attacker;
		private Character attackee;
		private CharacterView attackerView;
		private CharacterView attackeeView;
		private TickableManager tickableManager;
		private float timeRest;

		private AttackTurnAction (
			Character attacker,
			Character attackee,
			ICharacterViewResolver characterViewResolver,
			TickableManager tickableManager)
		{
			this.attacker = attacker;
			this.attackee = attackee;
			this.attackerView = characterViewResolver.Resolve (attacker);
			this.attackeeView = characterViewResolver.Resolve (attackee);
			this.tickableManager = tickableManager;
		}

		protected virtual void OnComplete (EventArgs e)
		{
			var handler = this.Complete;
			if (handler != null)
				handler (this, e);
		}

		public void Perform ()
		{
			attackerView.Look (attacker.Position.GetDirectionTo (attackee.Position));
			attackeeView.Look (attackee.Position.GetDirectionTo (attacker.Position));

			attackee.ApplyDamage (attacker.Damage);
			float attackDuration = attackerView.PlayAttack ();
			float reactionDuration = attackee.Hp > 0
				? attackeeView.PlayHurt ()
				: attackeeView.PlayDie ();

			timeRest = Mathf.Max (attackDuration, reactionDuration);
			tickableManager.Add (this);
		}

		public void Tick ()
		{
			timeRest -= Time.deltaTime;
			if (timeRest <= 0) {
				OnComplete (EventArgs.Empty);
				tickableManager.Remove (this);
			}
		}

		public Position TargetPosition {
			get {
				return attackee.Position;
			}
		}

		public event EventHandler<EventArgs> Complete;
	}
}