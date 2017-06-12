using Zenject;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using AssetBundles;

namespace Dust {
	public class SceneLauncher : ITickable
	{
		public interface IOptions
		{
			void ExtraBindings (DiContainer container);

			string Scene { get; }
			string SceneToUnload { get; }
		}

		private ZenjectAssetBundleSceneLoader zenjectSceneLoader;
		private TickableManager tickableManager;
		private AssetBundleLoadOperation loadOperation;

		private SceneLauncher (
			ZenjectAssetBundleSceneLoader zenjectSceneLoader,
			TickableManager tickableManager)
		{
			this.zenjectSceneLoader = zenjectSceneLoader;
			this.tickableManager = tickableManager;
		}

		public void Run (IOptions options)
		{
			loadOperation = zenjectSceneLoader.LoadSceneAsync (
				options.Scene, LoadSceneMode.Additive, options.ExtraBindings);

			tickableManager.Add (this);

			if (!string.IsNullOrEmpty (options.SceneToUnload))
				SceneManager.UnloadSceneAsync (options.SceneToUnload);
		}

		public void Tick ()
		{
			if (!loadOperation.IsDone ())
				return;

			tickableManager.Remove (this);
		}
	}
}