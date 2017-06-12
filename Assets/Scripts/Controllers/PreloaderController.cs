using UnityEngine;

namespace Dust.Controllers {
	public class PreloaderController : MonoBehaviour
	{
		public void Display ()
		{
			gameObject.SetActive (true);
		}

		public void Hide ()
		{
			gameObject.SetActive (false);
		}
	}
}