using Zenject;

namespace Dust.Views {
	public class FieldViewInstaller : MonoInstaller
	{
		public override void InstallBindings ()
		{
			Container
				.BindFactory<string, CharacterView, CharacterView.Factory> ()
				.FromSubContainerResolve ()
				.ByNewPrefabResource<CharacterViewInstaller> ("Characters/Character View")
				.UnderTransform (transform);

			Container
				.BindFactory<string, ObstacleView, ObstacleView.Factory> ()
				.FromSubContainerResolve ()
				.ByNewPrefabResource<ObstacleViewInstaller> ("Obstacles/Obstacle View")
				.UnderTransform (transform);

			Container
				.BindFactory<ExitView, ExitView.Factory> ()
				.FromSubContainerResolve ()
				.ByNewPrefabResource ("Exits/Exit View")
				.UnderTransform (transform);

			Container
				.Bind (typeof (FieldView), typeof (IPositionVerctor2Mapper))
				.To<FieldView> ()
				.FromNewComponentOn (gameObject)
				.AsSingle ();
		}
	}
}