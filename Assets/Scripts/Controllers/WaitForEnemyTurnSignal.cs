using Zenject;
using Dust.Models;

namespace Dust.Controllers
{
	public class WaitForEnemyTurnSignal : Signal<Character, WaitForEnemyTurnSignal>
	{
	}
}
