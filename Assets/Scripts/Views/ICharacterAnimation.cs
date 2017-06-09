
namespace Dust.Views.Animations {
	public interface ICharacterAnimation
	{
		float Move ();
		float Attack ();
		float Hurt ();
		float Die ();
	}
}