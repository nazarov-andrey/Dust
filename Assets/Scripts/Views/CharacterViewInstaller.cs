using Zenject;
using UnityEngine.UI;

namespace Dust.Views {
	public class CharacterViewInstaller : MonoInstaller
	{
		private string kind;

		[Inject]
		private void Inject (string kind)
		{
			this.kind = kind;
		}

		public override void InstallBindings ()
		{
			Container
				.Bind<Image> ()
				.FromNewComponentOnNewGameObject (gameObject)
				.AsSingle ()
				.NonLazy ();

			Container
				.Bind<
		}
	}
}