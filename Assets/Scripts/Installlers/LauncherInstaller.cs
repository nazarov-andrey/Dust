using Zenject;
using System.Collections;

namespace Dust {
	public class LauncherInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container
				.BindInterfacesAndSelfTo<EntryPoint> ()
				.AsSingle ();

			Container
				.Bind<SceneLauncher> ()
				.AsSingle ();

			Container
				.Bind<ZenjectAssetBundleSceneLoader> ()
				.AsSingle ();

			Container
				.BindInstance (AssetBundleNames.ScenesBundle)
				.WhenInjectedInto<ZenjectAssetBundleSceneLoader> ();

			Container
				.Bind (typeof (IInitializable), typeof (AssetBundleInitializer))
				.To<AssetBundleInitializer> ()
				.AsSingle ();

			Container
				.BindInstance (AssetBundleNames.ScenesBundle)
				.WhenInjectedInto<AssetBundleInitializer> ();

			Container
				.BindInstance (AssetBundleNames.GameplayBundle)
				.WhenInjectedInto<AssetBundleInitializer> ();
		}
	}
}