using Zenject;
using UnityEngine.UI;
using UnityEngine;
using AssetBundles;

namespace Dust.Views {
	public class ObstacleViewInstaller : MonoInstaller
	{
		private string kind;

		[Inject]
		private void Inject (string kind)
		{
			this.kind = kind;
		}

		private Sprite LoadSprite (string kind)
		{
			return AssetBundleManager.LoadAsset<Sprite> (AssetBundleNames.GameplayBundle, kind);
		}

		public override void InstallBindings ()
		{
			Container
				.Bind<ObstacleView> ()
				.AsSingle ();

			Container
				.Bind<Image> ()
				.FromComponentInHierarchy ();

			Container
				.Bind<Sprite> ()
				.FromMethod (x => LoadSprite (kind))
				.AsSingle ();

			Container
				.Bind<RectTransform> ()
				.FromMethod (x => gameObject.GetComponent<RectTransform> ());
		}
	}
}