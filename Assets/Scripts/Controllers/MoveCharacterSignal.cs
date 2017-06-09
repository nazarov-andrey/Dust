using Dust.Models;
using Zenject;

namespace Dust.Controllers {
	public class MoveCharacterSignal : Signal<ICharacterMover, MoveCharacterSignal>
	{
	}
}