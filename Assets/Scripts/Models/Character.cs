
namespace Dust.Models {
	public class Character : PositionHolder
	{
		private int hp;
		private int damage;
		private string kind;

		public Character (Position position, int hp, int damage, string kind) : base (position)
		{
			this.hp = hp;
			this.damage = damage;
			this.kind = kind;
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
	}
}