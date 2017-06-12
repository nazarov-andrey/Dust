using Zenject;
using UnityEngine;

namespace Dust {
	public class EntryPoint : IInitializable
	{
		private SceneLauncher sceneLauncher;

		private EntryPoint (SceneLauncher sceneLauncher)
		{
			this.sceneLauncher = sceneLauncher;
		}

		public void Initialize ()
		{
			sceneLauncher.Launch (SceneNames.Start);
		}
	}
}
