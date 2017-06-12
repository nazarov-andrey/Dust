#if !NOT_UNITY3D

using System;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using ModestTree;
using Zenject;
using AssetBundles;
using System.Collections.Generic;

namespace Dust
{
    public class ZenjectAssetBundleSceneLoader
    {
//    	class PendingSceneLoad
//    	{
//			public readonly string sceneName;
//			public readonly LoadSceneMode loadMode;
//			public readonly Action<DiContainer> extraBindings;
//			public readonly LoadSceneRelationship containerMode;
//
//			public PendingSceneLoad (
//				string sceneName,
//				LoadSceneMode loadMode,
//				Action<DiContainer> extraBindings,
//				LoadSceneRelationship containerMode)
//    		{
//    			this.sceneName = sceneName;
//    			this.loadMode = loadMode;
//    			this.extraBindings = extraBindings;
//    			this.containerMode = containerMode;
//    		}
//    	}

		class PendingLoadOperation : AssetBundleLoadOperation
		{
			private AssetBundleInitializer assetBundleInitializer;
			private ZenjectAssetBundleSceneLoader sceneLoader;
			private string sceneName;
			private LoadSceneMode loadMode;
			private Action<DiContainer> extraBindings;
			private LoadSceneRelationship containerMode;

			private AssetBundleLoadOperation loadOperation;

			public PendingLoadOperation (
				AssetBundleInitializer assetBundleInitializer,
				ZenjectAssetBundleSceneLoader sceneLoader,
				string sceneName,
				LoadSceneMode loadMode,
				Action<DiContainer> extraBindings,
				LoadSceneRelationship containerMode)
			{
				this.assetBundleInitializer = assetBundleInitializer;
				this.sceneLoader = sceneLoader;
				this.sceneName = sceneName;
				this.loadMode = loadMode;
				this.extraBindings = extraBindings;
				this.containerMode = containerMode;

				assetBundleInitializer.Initialized += AssetBundleInitializerInitialized;
			}

			private void AssetBundleInitializerInitialized (object sender, EventArgs e)
			{
				assetBundleInitializer.Initialized -= AssetBundleInitializerInitialized;
				loadOperation = sceneLoader.LoadSceneAsync (
					sceneName, loadMode, extraBindings, containerMode);
			}

			public override bool Update ()
			{
				if (loadOperation == null)
					return true;

				return loadOperation.Update ();
			}

			public override bool IsDone ()
			{
				return loadOperation != null && loadOperation.IsDone ();
			}
		}		

        readonly ProjectKernel _projectKernel;
        readonly DiContainer _sceneContainer;
        readonly string _assetBundleName;
		readonly AssetBundleInitializer _assetBundleInitializer;
//		readonly Queue<PendingSceneLoad> _pendingSceneLoads;

		public ZenjectAssetBundleSceneLoader(
            SceneContext sceneRoot,
            ProjectKernel projectKernel,
			AssetBundleInitializer assetBundleInitializer,
			string assetBundleName)
        {
            _projectKernel = projectKernel;
            _sceneContainer = sceneRoot.Container;
			_assetBundleInitializer = assetBundleInitializer;
			_assetBundleName = assetBundleName;
        }

		public AssetBundleLoadOperation LoadSceneAsync(string sceneName)
        {
            return LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }

		public AssetBundleLoadOperation LoadSceneAsync(string sceneName, LoadSceneMode loadMode)
        {
            return LoadSceneAsync(sceneName, loadMode, null);
        }

		public AssetBundleLoadOperation LoadSceneAsync(
            string sceneName, LoadSceneMode loadMode, Action<DiContainer> extraBindings)
        {
            return LoadSceneAsync(sceneName, loadMode, extraBindings, LoadSceneRelationship.None);
        }

		public AssetBundleLoadOperation LoadSceneAsync(
            string sceneName,
            LoadSceneMode loadMode,
            Action<DiContainer> extraBindings,
            LoadSceneRelationship containerMode)
        {
    		if (!_assetBundleInitializer.IsInitialized) {
    			return new PendingLoadOperation (
    				_assetBundleInitializer,
    				this,
    				sceneName,
    				loadMode,
    				extraBindings,
    				containerMode);
    		}

            PrepareForLoadScene(loadMode, extraBindings, containerMode);

            return AssetBundleManager.LoadLevelAsync (
            	_assetBundleName,
            	sceneName,
            	loadMode == LoadSceneMode.Additive);
        }

        void PrepareForLoadScene(
            LoadSceneMode loadMode,
            Action<DiContainer> extraBindings,
            LoadSceneRelationship containerMode)
        {
            if (loadMode == LoadSceneMode.Single)
            {
                Assert.IsEqual(containerMode, LoadSceneRelationship.None);

                // Here we explicitly unload all existing scenes rather than relying on Unity to
                // do this for us.  The reason we do this is to ensure a deterministic destruction
                // order for everything in the scene and in the container.
                // See comment at ProjectKernel.OnApplicationQuit for more details
                _projectKernel.ForceUnloadAllScenes();
            }

            if (containerMode == LoadSceneRelationship.None)
            {
                SceneContext.ParentContainer = null;
            }
            else if (containerMode == LoadSceneRelationship.Child)
            {
                SceneContext.ParentContainer = _sceneContainer;
            }
            else
            {
                Assert.IsEqual(containerMode, LoadSceneRelationship.Sibling);
                SceneContext.ParentContainer = _sceneContainer.ParentContainer;
            }

            SceneContext.ExtraBindingsInstallMethod = extraBindings;
        }
    }
}

#endif
