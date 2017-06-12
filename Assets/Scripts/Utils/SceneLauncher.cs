using Zenject;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using AssetBundles;
using Dust.Controllers;

namespace Dust {
	public class SceneLauncher : ITickable
	{
		interface ILoadOperation
		{
			bool IsDone { get; }
		}

		class AsyncOperationLoadOperation : ILoadOperation
		{
			private AsyncOperation asyncOperation;

			public AsyncOperationLoadOperation (AsyncOperation asyncOperation)
			{
				this.asyncOperation = asyncOperation;
			}

			public bool IsDone {
				get {
					return asyncOperation.isDone;
				}
			}
		}

		class AssetBundleLoadOperation : ILoadOperation
		{
			private AssetBundles.AssetBundleLoadOperation assetBundleLoadOperation;

			public AssetBundleLoadOperation (AssetBundles.AssetBundleLoadOperation assetBundleLoadOperation)
			{
				this.assetBundleLoadOperation = assetBundleLoadOperation;
			}

			public bool IsDone {
				get {
					return assetBundleLoadOperation.IsDone ();
				}
			}
		}
			


		private ZenjectAssetBundleSceneLoader zenjectAssetBundleSceneLoader;
		private ZenjectSceneLoader zenjectSceneLoader;
		private TickableManager tickableManager;
		private ILoadOperation loadOperation;
		private PreloaderController preloaderController;
		private string currentScene;

		private SceneLauncher (
			ZenjectAssetBundleSceneLoader zenjectAssetBundleSceneLoader,
			ZenjectSceneLoader zenjectSceneLoader,
			TickableManager tickableManager,
			PreloaderController preloaderController)
		{
			this.zenjectAssetBundleSceneLoader = zenjectAssetBundleSceneLoader;
			this.zenjectSceneLoader = zenjectSceneLoader;
			this.tickableManager = tickableManager;
			this.preloaderController = preloaderController;
		}

		private bool IsBuildInScene (string sceneName)
		{
			for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
				string[] chunks = SceneUtility
					.GetScenePathByBuildIndex (i)
					.Split ('/');

				if (chunks [chunks.Length - 1] == sceneName + ".unity")
					return true;
 			}

			return false;
		}

		public void Launch (
			string scene,
			bool unloadCurrent = true,
			Action<DiContainer> extraBindings = null)
		{
			if (unloadCurrent && !string.IsNullOrEmpty (currentScene))
				SceneManager.UnloadSceneAsync (currentScene);

			currentScene = scene;
			if (IsBuildInScene (scene))
				loadOperation =
					new AsyncOperationLoadOperation (
						zenjectSceneLoader.LoadSceneAsync (scene, LoadSceneMode.Additive, extraBindings));
			else
				loadOperation =
					new AssetBundleLoadOperation (
						zenjectAssetBundleSceneLoader.LoadSceneAsync (scene, LoadSceneMode.Additive, extraBindings));				

			preloaderController.Display ();
			tickableManager.Add (this);
		}

		public void Tick ()
		{
			if (!loadOperation.IsDone)
				return;

			preloaderController.Hide ();
			tickableManager.Remove (this);
		}
	}
}