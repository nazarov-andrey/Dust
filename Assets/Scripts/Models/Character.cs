
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

		public override string ToString ()
		{
			return string.Format ("[Character: id={0}, kind={1}]", id, kind);
		}
		
	}
}