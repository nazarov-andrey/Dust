using UnityEngine;
using Dust.Models;

namespace Dust.Views {
	public interface IPositionVerctor2Mapper
	{
		Vector2 Map (Position position);
	}
}
