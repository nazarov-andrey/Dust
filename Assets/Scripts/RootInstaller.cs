using Zenject;
using Dust.Models;
using Dust.Controllers;

namespace Dust {
	public class RootInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container
				.Bind<Field> ()
				.To<PredefinedField> ()
				.AsSingle ();

			Container
				.Bind<FieldController> ()
				.AsSingle ()
				.NonLazy ();
		}
	}
}