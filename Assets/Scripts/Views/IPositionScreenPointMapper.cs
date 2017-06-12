using UnityEngine;
using Dust.Models;

namespace Dust.Views {
	public interface IPositionScreenPointMapper
	{
		Vector2 PositionToScreenPoint (Position position);
		Position ScreenPointToPosition (Vector2 screenPoint);
	}
}
