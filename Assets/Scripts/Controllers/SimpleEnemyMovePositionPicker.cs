using Dust.Models;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Dust.Controllers {
	public class SimpleEnemyMovePositionPicker : IEnemyMovePositionPicker
	{
		class PositionDistancePair : IComparable<PositionDistancePair>
		{
			private Position position;
			private float distance;

			public PositionDistancePair (Field field, Position position)
			{
				this.position = position;
				this.distance = field.Player.Position.Sub (position).Magnitude;
			}

			public int CompareTo (PositionDistancePair other)
			{
				return distance.CompareTo (other.distance);
			}

			public Position Position {
				get {
					return this.position;
				}
			}

			public float Distance {
				get {
					return this.distance;
				}
			}
		}

		private Field field;

		private SimpleEnemyMovePositionPicker (Field field)
		{
			this.field = field;
		}

		public Position PickPosition (Character character)
		{
			List<PositionDistancePair> positionDistancePairs =
				field
					.GetFreeNeighbourPositions (character.Position)
					.ConvertAll (x => new PositionDistancePair (field, x));

			if (positionDistancePairs.Count == 0)
				return null;

			positionDistancePairs.Sort ();
			return positionDistancePairs [0].Position;
		}
	}
}
