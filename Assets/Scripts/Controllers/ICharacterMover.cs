using Dust.Models;
using Dust.Views;

namespace Dust.Controllers {
	public interface ICharacterMover
	{
		void Run ();
		Position Destination { get; }
	}
}
