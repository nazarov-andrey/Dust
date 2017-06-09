using System.Collections.Generic;

namespace Dust.Models {
	public class Field
	{
		private int width;
		private int height;
		private Position exit;
		private Character player;
		private List<Character> enemies;
		private List<Obstacle> obstacles;
		private List<PositionHolder> positionHolders;

		public Field (
			int width,
			int height,
			Position exit,
			Character player,
			IEnumerable<Character> enemies,
			IEnumerable<Obstacle> obstacles)
		{
			this.width = width;
			this.height = height;
			this.exit = exit;
			this.player = player;
			this.enemies = new List<Character> (enemies);
			this.obstacles = new List<Obstacle> (obstacles);

			positionHolders = new List<PositionHolder> (enemies as IEnumerable<PositionHolder>);
			positionHolders.AddRange (obstacles as IEnumerable<PositionHolder>);
			positionHolders.Add (player);
		}

		public bool IsPositionOccupied (Position position)
		{
			return positionHolders.Exists (x => x.Position.Equals (position));
		}

		public int Width {
			get {
				return this.width;
			}
		}

		public int Height {
			get {
				return this.height;
			}
		}

		public Character Player {
			get {
				return this.player;
			}
		}

		public List<Character> Enemies {
			get {
				return this.enemies;
			}
		}

		public List<Obstacle> Obstacles {
			get {
				return this.obstacles;
			}
		}

		public Position Exit {
			get {
				return this.exit;
			}
		}
	}
}