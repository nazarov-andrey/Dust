using UnityEngine;
using Zenject;
using System;
using Dust.Models;

namespace Dust.Views
{
	public abstract class OnGridView
	{
		class MoveTask : ITickable
		{
			private RectTransform rectTransform;
			private Vector2 position;
			private float time;
			private TickableManager tickableManager;
			private bool stopped;
			private Vector2 velocity;

			public MoveTask (
				RectTransform rectTransform,
				Vector2 position,
				float time,
				TickableManager tickableManager)
			{
				this.rectTransform = rectTransform;
				this.position = position;
				this.time = time;
				this.tickableManager = tickableManager;

				tickableManager.Add (this);
			}

			protected virtual void OnComplete (EventArgs e)
			{
				var handler = this.Complete;
				if (handler != null)
					handler (this, e);
			}

			public void Tick ()
			{
				rectTransform.anchoredPosition = Vector2.SmoothDamp (
					rectTransform.anchoredPosition,
					position,
					ref velocity,
					time,
					float.MaxValue,
					Time.deltaTime);

				time -= Time.deltaTime;
				if (time <= 0) {
					OnComplete (EventArgs.Empty);
					Stop ();
				}
			}

			public void Stop ()
			{
				if (stopped)
					return;

				tickableManager.Remove (this);
				stopped = true;
				Complete = null;
			}

			public event EventHandler<EventArgs> Complete;
		}

		private RectTransform rectTransform;
		private TickableManager tickableManager;
		private MoveTask moveTask;

		[Inject]
		private void Inject (
			RectTransform rectTransform,
			TickableManager tickableManager)
		{
			this.rectTransform = rectTransform;
			this.tickableManager = tickableManager;
		}

		private void MoveTaskComplete (object sender, EventArgs e)
		{
			OnMoved (EventArgs.Empty);
		}

		protected virtual void OnMoved (EventArgs e)
		{
			var handler = this.Moved;
			if (handler != null)
				handler (this, e);
		}

		public void Place (Vector2 position)
		{
			rectTransform.anchoredPosition = position;
		}

		public void Move (Vector2 position, float time)
		{
			if (moveTask != null)
				moveTask.Stop ();

			moveTask = new MoveTask (rectTransform, position, time, tickableManager);
			moveTask.Complete += MoveTaskComplete;
		}

		public void Loot (Direction direction)
		{
			switch (direction)
			{
			case Direction.Left:
				rectTransform.localScale = new Vector2 (-1f, 1f);
				break;

			case Direction.Right:
				rectTransform.localScale = Vector2.one;
				break;

			default:
				break;
			}	
		}

		public event EventHandler<EventArgs> Moved;
	}
}