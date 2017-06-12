using UnityEngine;
using Dust.Models;
using Zenject;

namespace Dust.Views {
	public class FieldView : MonoBehaviour, IPositionScreenPointMapper
	{
		private float cellWidth;
		private float cellHeight;

		[Inject]
		private void Inject (Field field)
		{
			RectTransform rectTransform = transform as RectTransform;

			this.cellWidth = rectTransform.rect.width / (float)field.Width;
			this.cellHeight = rectTransform.rect.height / (float)field.Height;
		}

		public Vector2 PositionToScreenPoint (Position position)
		{
			return new Vector2 (
				position.Col * cellWidth,
				position.Row * cellHeight);
		}

		public Position ScreenPointToPosition (Vector2 screenPoint)
		{
			Vector2 localPoint = transform.InverseTransformPoint (screenPoint);

			return new Position (
				Mathf.CeilToInt ((localPoint.x - cellWidth / 2 ) / cellWidth),
				Mathf.CeilToInt ((localPoint.y - cellHeight / 2 ) / cellHeight));
		}
	}
}