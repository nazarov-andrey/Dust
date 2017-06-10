using Dust.Models;

namespace Dust.Controllers {
	public interface IEnemyMovePositionPicker
	{
		Position PickPosition (Character character);
	}
}
