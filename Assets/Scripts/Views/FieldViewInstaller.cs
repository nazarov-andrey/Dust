using Zenject;
using AssetBundles;
using UnityEngine;

namespace Dust.Views {
	public class FieldViewInstaller : MonoInstaller
	{
//		private AssetBundle GetGameplayAssetBundle ()
//		{
//			string error;
//			return AssetBundleManager.GetLoadedAssetBundle (
//				AssetBundleNames.GameplayBundle, out error).m_AssetBundle;
//		}

		private GameObject LoadAssetBundlePrefab (string name)
		{
			return AssetBundleManager.LoadAsset<GameObject> (
				AssetBundleNames.GameplayBundle, name);
		}

		public override void InstallBindings ()
		{
			Container
				.BindFactory<string, CharacterView, CharacterView.Factory> ()
				.FromSubContainerResolve ()
				.ByNewPrefab<CharacterViewInstaller> (LoadAssetBundlePrefab ("Character View"))
				.UnderTransform (transform);

			Container
				.BindFactory<string, ObstacleView, ObstacleView.Factory> ()
				.FromSubContainerResolve ()
				.ByNewPrefab<ObstacleViewInstaller> (LoadAssetBundlePrefab ("Obstacle View"))
				.UnderTransform (transform);

			Container
				.BindFactory<ExitView, ExitView.Factory> ()
				.FromSubContainerResolve ()
				.ByNewPrefab (LoadAssetBundlePrefab ("Exit View"))
				.UnderTransform (transform);

			Container
				.Bind (typeof (FieldView), typeof (IPositionScreenPointMapper))
				.To<FieldView> ()
				.FromNewComponentOn (gameObject)
				.AsSingle ();
		}
	}
}