using UnityEngine;

namespace Dust.Views
{
	public abstract class OnGridView
	{
		private RectTransform rectTransform;

		protected OnGridView (RectTransform rectTransform)
		{
			this.rectTransform = rectTransform;
		}

		public void Place (Vector2 position)
		{
			rectTransform.anchoredPosition = position;
		}
	}
}