using Zenject;
using Dust.Models;
using Dust.Controllers;
using UnityEngine;

namespace Dust {
	public class GameplayInstaller : MonoInstaller
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

			if (Application.isMobilePlatform) {
				Container
					.BindInterfacesAndSelfTo<FlickPlayerController> ()
					.AsSingle ();

				Container
					.BindInstance (10f)
					.WhenInjectedInto<FlickPlayerController> ();
			} else {
				Container
					.BindInterfacesAndSelfTo<TapPlayerController> ()
					.AsSingle ();
			}

			Container
				.BindInterfacesAndSelfTo<EnemyController> ()
				.AsSingle ();

			Container
				.Bind<IAttackPossibility> ()
				.To<NeighboursAttackPossibility> ()
				.AsSingle ();

			Container
				.Bind<IEnemyMovePositionPicker> ()
				.To<SimpleEnemyMovePositionPicker> ()
				.AsSingle ();

			Container
				.BindInterfacesAndSelfTo<GameController> ()
				.AsSingle ();

			Container
				.Bind<ILossCondtion> ()
				.To<PlayerDeadLossCondition> ()
				.AsSingle ();

			Container
				.Bind<IVictoryCondition> ()
				.To<ExitReachedVictoryCondition> ()
				.AsSingle ();

			Container
				.Bind<Character> ()
				.FromMethod (x => x.Container.Resolve<Field> ().Player)
				.WhenInjectedInto<FlickPlayerController> ();

			Container
				.BindFactory<Character, Direction, MoveTurnAction, MoveTurnAction.DirectionFactory> ();

			Container
				.BindFactory<Character, Position, MoveTurnAction, MoveTurnAction.PositionFactory> ();

			Container
				.BindFactory<Character, Character, AttackTurnAction, AttackTurnAction.Factory> ();

			Container.DeclareSignal<PlayerTurnSignal> ();
			Container.DeclareSignal<EnemyTurnSignal> ();
			Container.DeclareSignal<WaitForPlayerTurnSignal> ();
			Container.DeclareSignal<WaitForEnemyTurnSignal> ();
			Container.DeclareSignal<RearrangeViewsSignal> ();
		}
	}
}