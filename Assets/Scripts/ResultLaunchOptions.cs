using Zenject;
using Dust.Controllers;

namespace Dust {
	public class ResultLaunchOptions : SceneLauncher.IOptions
	{
		private GameResult gameResult;

		public ResultLaunchOptions (GameResult gameResult)
		{
			this.gameResult = gameResult;
		}

		public void ExtraBindings (DiContainer container)
		{
			container.BindInstance (gameResult);
		}

		public string Scene {
			get {
				return SceneNames.Result;
			}
		}

		public string SceneToUnload {
			get {
				return SceneNames.Gameplay;
			}
		}
	}
}