﻿using Zenject;
using UnityEngine;

namespace Dust.Views {
	public class ExitView : OnGridView
	{
		public class Factory : Factory<ExitView>
		{
		}

		private ExitView (RectTransform rectTransform) : base (rectTransform)
		{
		}
	}
}