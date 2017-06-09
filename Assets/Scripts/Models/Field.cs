using System.Collections.Generic;

namespace Dust.Models {
	public class Field
	{
		private int width;
		private int height;
		private Exit exit;
		private Character player;
		private List<Character> enemies;
		private List<Obstacle> obstacles;
		private List<PositionHolder> positionHolders;
		private Position halfSize;
		private List<Position> corners;

		public Field (
			int width,
			int height,
			Exit exit,
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
			this.halfSize = new Position (width / 2, height / 2);

			positionHolders = new List<PositionHolder> ();
			positionHolders.Add (player);
			positionHolders.Add (exit);
			positionHolders.AddRange (obstacles as IEnumerable<PositionHolder>);
			positionHolders.AddRange (enemies as IEnumerable<PositionHolder>);

			corners = new List<Position> {
				new Position (0, 0),
				new Position (0, height - 1),
				new Position (width - 1, height - 1),
				new Position (width - 1, 0) };
		}

		private bool IsCorner (Position shiftedPosition)
		{
			return corners.Contains (shiftedPosition);
		}

		private bool IsInBounds (Position shiftedPosition)
		{
			return 0 <= shiftedPosition.Col && shiftedPosition.Col < width
				&& 0 <= shiftedPosition.Row && shiftedPosition.Row < height;
		}

		public bool TryGetPositionHolder (Position position, out PositionHolder positionHoder)
		{
			positionHoder = positionHolders.Find (x => x.Position.Equals (position));
			Character enemy = positionHoder as Character;

			return positionHoder != null && (enemy == null || enemy.IsAlive);
		}

		public bool IsPositionValid (Position position)
		{
			Position shifted = position.Add (halfSize);
			return IsInBounds (shifted) && !IsCorner (shifted);
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

		public Exit Exit {
			get {
				return this.exit;
			}
		}
	}
}