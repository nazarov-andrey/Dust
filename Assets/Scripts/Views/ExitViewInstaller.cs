using Zenject;
using UnityEngine;

namespace Dust.Views {
	public class ExitViewInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container
				.Bind<ExitView> ()
				.AsSingle ();

			Container
				.Bind<RectTransform> ()
				.FromMethod (x => GetComponent<RectTransform> ());
		}
	}
}