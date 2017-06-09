using UnityEngine;
using Dust.Models;
using Zenject;

namespace Dust.Views {
	public class FieldView : MonoBehaviour, IPositionVerctor2Mapper
	{
		private float cellWidth;
		private float cellHeight;

		[Inject]
		private void Inject (Field field)
		{
			RectTransform rectTransform = transform as RectTransform;

			this.cellWidth = rectTransform.rect.width / (float)field.Width;
			this.cellHeight = rectTransform.rect.width / (float)field.Height;
		}

		public Vector2 Map (Position position)
		{
			return new Vector2 (
				position.Row * cellWidth,
				position.Col * cellHeight);
		}
	}
}