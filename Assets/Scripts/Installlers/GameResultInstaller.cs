using Zenject;
using UnityEngine.UI;
using Dust.Controllers;
using UnityEngine;

namespace Dust {
	public class GameResultInstaller : MonoInstaller
	{
		private GameResult gameResult;

		public override void InstallBindings ()
		{
			Container
				.BindInterfacesAndSelfTo<ResultLogoController> ()
				.AsSingle ();

			Container
				.BindInstance ("Victory")
				.WithId (ResultLogoController.VictorySprite);

			Container
				.BindInstance ("Loss")
				.WithId (ResultLogoController.LossSprite);
		}
	}
}