using Dust.Models;
using Zenject;

namespace Dust.Controllers {
	public class PlayerTurnSignal : Signal<ITurnAction, PlayerTurnSignal>
	{
	}
}