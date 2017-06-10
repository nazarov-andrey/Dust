using Zenject;

namespace Dust {
	public class LauncherInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container
				.Bind<SceneLauncher> ()
				.AsSingle ();

			
		}
	}
}