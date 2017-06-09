using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dust.Views {
	public class ObstacleView : OnGridView
	{
		public class Factory : Factory<string, ObstacleView>
		{
		}

		private Image image;
		private Sprite sprite;

		private ObstacleView (
				RectTransform rectTransform,
				Image image,
				Sprite sprite)

			: base (rectTransform)
		{
			image.overrideSprite = sprite;
		}
	}
}