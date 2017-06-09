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
				.BindInterfacesAndSelfTo<FieldController> ()
				.AsSingle ();

			Container
				.BindInterfacesAndSelfTo<PlayerController> ()
				.AsSingle ();

			Container
				.BindInterfacesAndSelfTo<GameController> ()
				.AsSingle ();

			Container
				.Bind<Character> ()
				.FromMethod (x => x.Container.Resolve<Field> ().Player)
				.WhenInjectedInto<PlayerController> ();

			Container
				.BindFactory<Character, Direction, DefaultCharacterMover, DefaultCharacterMover.Factory> ();

			Container
				.DeclareSignal<MoveCharacterSignal> ();
		}
	}
}