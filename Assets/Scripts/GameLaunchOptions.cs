using Zenject;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dust {
	public class GameLaunchOptions : SceneLauncher.IOptions
	{
		private string sceneToUnload;

		public GameLaunchOptions (string sceneToUnload)
		{
			this.sceneToUnload = sceneToUnload;
		}

		public void ExtraBindings (DiContainer container)
		{
		}

		public string Scene {
			get {
				return SceneNames.Gameplay;
			}
		}

		public string SceneToUnload {
			get {
				return sceneToUnload;
			}
		}
	}
}