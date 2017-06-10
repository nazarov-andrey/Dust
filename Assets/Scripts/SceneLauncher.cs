using Zenject;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Dust {
	public class SceneLauncher : ITickable
	{
		public interface IOptions
		{
			void ExtraBindings (DiContainer container);

			string Scene { get; }
			string SceneToUnload { get; }
		}

		private ZenjectSceneLoader zenjectSceneLoader;
		private TickableManager tickableManager;
		private AsyncOperation asyncOperation;

		private SceneLauncher (
			ZenjectSceneLoader zenjectSceneLoader,
			TickableManager tickableManager)
		{
			this.zenjectSceneLoader = zenjectSceneLoader;
			this.tickableManager = tickableManager;
		}

		public void Run (IOptions options)
		{
			asyncOperation = zenjectSceneLoader.LoadSceneAsync (
				options.Scene,
				LoadSceneMode.Additive,
				options.ExtraBindings);

			tickableManager.Add (this);

			if (!string.IsNullOrEmpty (options.SceneToUnload))
				SceneManager.UnloadSceneAsync (options.SceneToUnload);
		}

		public void Tick ()
		{
			if (!asyncOperation.isDone)
				return;

			tickableManager.Remove (this);
		}
	}
}