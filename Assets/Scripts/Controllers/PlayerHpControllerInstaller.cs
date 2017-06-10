using Zenject;
using TMPro;

namespace Dust.Controllers {
	public class PlayerHpControllerInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container
				.Bind<TextMeshProUGUI> ()
				.FromComponentInHierarchy ();

			Container
				.BindInterfacesAndSelfTo<PlayerHpController> ()
				.AsSingle ();
		}
	}
}