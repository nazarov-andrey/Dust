using Zenject;
using Dust.Models;
using System.Collections.Generic;

namespace Dust.Controllers
{
	public class GameController : IInitializable
	{
		private Field field;
		private PlayerTurnSignal playerTurnSignal;
		private EnemyTurnSignal enemyTurnSignal;
		private WaitForPlayerTurnSignal waitForPlayerTurnSignal;
		private WaitForEnemyTurnSignal waitForEnemyTurnSignal;
		private AttackTurnAction.Factory attackPerformerFactory;
		private Queue<Character> turnQueue;

		private GameController (
			Field field,
			PlayerTurnSignal playerTurnSignal,
			EnemyTurnSignal enemyTurnSignal,
			WaitForPlayerTurnSignal waitForPlayerTurnSignal,
			WaitForEnemyTurnSignal waitForEnemyTurnSignal,
			AttackTurnAction.Factory attackPerformerFactory)
		{
			this.field = field;
			this.playerTurnSignal = playerTurnSignal;
			this.enemyTurnSignal = enemyTurnSignal;
			this.waitForPlayerTurnSignal = waitForPlayerTurnSignal;
			this.waitForEnemyTurnSignal = waitForEnemyTurnSignal;
			this.attackPerformerFactory = attackPerformerFactory;
			this.turnQueue = new Queue<Character> ();

			turnQueue.Enqueue (field.Player);
			foreach (var enemy in field.Enemies) {
				turnQueue.Enqueue (enemy);
			}
		}

		private void PlayerTurnSignalListener (ITurnAction turnAction)
		{
			if (!(turnAction is MoveTurnAction))
				throw new System.ArgumentException (
					"Expecting only " + typeof (MoveTurnAction) + " as player turn action");

			Position destination = turnAction.TargetPosition;
			if (!field.IsPositionValid (destination))
				return;

			PositionHolder positionHolder;
			if (!field.TryGetPositionHolder (turnAction.TargetPosition, out positionHolder)
				|| positionHolder is Exit)
			{
				turnAction.Perform ();
				turnAction.Complete += TurnActionComplete;
				return;
			}

			Character enemy = positionHolder as Character;
			if (enemy != null) {
				AttackTurnAction attackPerformer = attackPerformerFactory.Create (
					field.Player, enemy);
				attackPerformer.Perform ();
				attackPerformer.Complete += TurnActionComplete;

				return;
			}
		}

		private void TurnActionComplete (object sender, System.EventArgs e)
		{
			ITurnAction turnAction = sender as ITurnAction;
			if (turnAction == null)
				throw new System.ArgumentException (
					"sender should be of type " + typeof (ITurnAction));

			turnAction.Complete -= TurnActionComplete;
			ProcessNextTurn ();
		}

		private void EnemyTurnSignalListener (ITurnAction turnAction)
		{
			turnAction.Complete += TurnActionComplete;
			turnAction.Perform ();
		}

		private void ProcessNextTurn ()
		{
			Character turnOwner;
			do {
				turnOwner = turnQueue.Dequeue ();
			} while (!turnOwner.IsAlive);

			turnQueue.Enqueue (turnOwner);

			if (field.Player == turnOwner)
				waitForPlayerTurnSignal.Fire ();
			else
				waitForEnemyTurnSignal.Fire (turnOwner);
		}

		public void Initialize ()
		{
			playerTurnSignal.Listen (PlayerTurnSignalListener);
			enemyTurnSignal.Listen (EnemyTurnSignalListener);
			ProcessNextTurn ();
		}
	}
}
