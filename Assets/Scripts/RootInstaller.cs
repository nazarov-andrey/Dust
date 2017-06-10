﻿using Zenject;
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
				.BindInstance (10f)
				.WhenInjectedInto<PlayerController> ();

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
				.Bind<Character> ()
				.FromMethod (x => x.Container.Resolve<Field> ().Player)
				.WhenInjectedInto<PlayerController> ();

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