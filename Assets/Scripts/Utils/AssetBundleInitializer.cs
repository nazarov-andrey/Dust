using Zenject;
using AssetBundles;
using System;
using System.Collections.Generic;

namespace Dust {
	public class AssetBundleInitializer : IInitializable, ILateTickable
	{
		private TickableManager tickableManager;
		private AssetBundleLoadManifestOperation loadOperation;
		private Queue<string> bundlesToLoad;
		private string loadingNowBundle;
		private bool isInitialized;

		private AssetBundleInitializer (
			TickableManager tickableManager,
			List<string> bundlesToLoad)
		{
			this.tickableManager = tickableManager;
			this.bundlesToLoad = new Queue<string> (bundlesToLoad);
		}

		protected virtual void OnInitialized (EventArgs e)
		{
			var handler = this.Initialized;
			if (handler != null)
				handler (this, e);
		}

		public void Initialize ()
		{
			AssetBundles.AssetBundleManager.SetSourceAssetBundleDirectory (
				"/AssetBundles/" + Utility.GetPlatformName () + "/");
			loadOperation = AssetBundleManager.Initialize ();
			tickableManager.AddLate (this);
		}

		public void LateTick ()
		{
			bool manifestLoaded = loadOperation == null || loadOperation.IsDone ();
			if (!manifestLoaded)
				return;

			if (bundlesToLoad.Count == 0) {
				tickableManager.RemoveLate (this);
				isInitialized = true;
				OnInitialized (EventArgs.Empty);
				return;
			}

			if (loadingNowBundle != null) {
				string error;
				if (AssetBundleManager.GetLoadedAssetBundle (loadingNowBundle, out error) != null)
					loadingNowBundle = null;
			}

			loadingNowBundle = bundlesToLoad.Dequeue ();
			AssetBundleManager.LoadAssetBundle (loadingNowBundle);
		}

		public bool IsInitialized {
			get {
				return isInitialized;
			}
		}

		public event EventHandler<EventArgs> Initialized;
	}
}