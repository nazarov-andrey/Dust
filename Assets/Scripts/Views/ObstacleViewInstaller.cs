using Zenject;
using UnityEngine.UI;
using UnityEngine;

namespace Dust.Views {
	public class ObstacleViewInstaller : MonoInstaller
	{
		private string kind;

		[Inject]
		private void Inject (string kind)
		{
			this.kind = kind;
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
				.FromResource ("Obstacles/" + kind)
				.AsSingle ();

			Container
				.Bind<RectTransform> ()
				.FromMethod (x => gameObject.GetComponent<RectTransform> ());
		}
	}
}