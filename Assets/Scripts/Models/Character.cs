using System;


namespace Dust.Models {
	public class Character : PositionHolder
	{
		private static int nextId = 0;

		private int id;
		private int hp;
		private int damage;
		private string kind;

		public Character (Position position, int hp, int damage, string kind) : base (position)
		{
			this.id = nextId++;
			this.hp = hp;
			this.damage = damage;
			this.kind = kind;
		}

		protected virtual void OnHit (EventArgs e)
		{
			var handler = this.Hit;
			if (handler != null)
				handler (this, e);
		}

		public void ApplyDamage (int damage)
		{
			hp -= damage;
			if (hp < 0)
				hp = 0;

			OnHit (EventArgs.Empty);
		}

		public override string ToString ()
		{
			return string.Format ("[Character: id={0}, kind={1}]", id, kind);
		}

		public override bool Equals (object obj)
		{
			if (obj == null)
				return false;
			if (ReferenceEquals (this, obj))
				return true;
			if (obj.GetType () != typeof(Character))
				return false;
			Character other = (Character)obj;
			return id == other.id;
		}

		public override int GetHashCode ()
		{
			unchecked {
				return id.GetHashCode ();
			}
		}

		public int Hp {
			get {
				return this.hp;
			}
		}

		public int Damage {
			get {
				return this.damage;
			}
		}

		public string Kind {
			get {
				return this.kind;
			}
		}

		public bool IsAlive {
			get {
				return this.hp > 0;
			}
		}

		public override int ViewWeight {
			get {
				int result = base.ViewWeight;
				if (!IsAlive)
					result -= 1;

				return result;
			}
		}

		public override bool IsActive {
			get {
				return base.IsActive && IsAlive;
			}
		}

		public event EventHandler<EventArgs> Hit;
	}
}