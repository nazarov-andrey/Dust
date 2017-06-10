using Zenject;
using Dust.Models;

namespace Dust.Controllers {
	public class EnemyController : IInitializable
	{
		private Field field;
		private WaitForEnemyTurnSignal waitForEnemyTurnSignal;
		private EnemyTurnSignal enemyTurnSignal;
		private AttackTurnAction.Factory attackTurnActionFactory;
		private MoveTurnAction.PositionFactory moveTurnActionFactory;
		private IAttackPossibility attackPossibility;
		private IEnemyMovePositionPicker enemyMovePositionPicker;

		private EnemyController (
			Field field,
			WaitForEnemyTurnSignal waitForEnemyTurnSignal,
			EnemyTurnSignal enemyTurnSignal,
			AttackTurnAction.Factory attackTurnActionFactory,
			MoveTurnAction.PositionFactory moveTurnActionFactory,
			IAttackPossibility attackPossibility,
			IEnemyMovePositionPicker enemyMovePositionPicker)
		{
			this.field = field;
			this.waitForEnemyTurnSignal = waitForEnemyTurnSignal;
			this.enemyTurnSignal = enemyTurnSignal;
			this.attackTurnActionFactory = attackTurnActionFactory;
			this.moveTurnActionFactory = moveTurnActionFactory;
			this.attackPossibility = attackPossibility;
			this.enemyMovePositionPicker = enemyMovePositionPicker;
		}

		private void WaitForEnemyTurnSignal (Character enemy)
		{
			Character player = field.Player;
			ITurnAction turnAction =
				attackPossibility.IsPossible (enemy, player)
					? attackTurnActionFactory.Create (enemy, player) as ITurnAction
					: moveTurnActionFactory.Create (
						enemy, enemyMovePositionPicker.PickPosition (enemy));

			enemyTurnSignal.Fire (turnAction);
		}

		public void Initialize ()
		{
			waitForEnemyTurnSignal.Listen (WaitForEnemyTurnSignal);	
		}
	}
}
