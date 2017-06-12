using UnityEngine;
using Zenject;

namespace Dust.Controllers {
	public class LaunchGameButtonController : MonoBehaviour
	{
		[SerializeField]
		private string sceneToUnload;
		private SceneLauncher sceneLauncher;

		[Inject]
		private void Inject (SceneLauncher sceneLauncher)
		{
			this.sceneLauncher = sceneLauncher;
		}

		public void OnClick ()
		{
			sceneLauncher.Launch (SceneNames.Gameplay);
		}
	}
}