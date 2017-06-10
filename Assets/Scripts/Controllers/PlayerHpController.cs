using Dust.Models;
using TMPro;
using Zenject;
using UnityEngine;

namespace Dust.Controllers {
	public class PlayerHpController : IInitializable
	{
		private Character player;
		private TextMeshProUGUI text;

		private PlayerHpController (
			Field field,
			TextMeshProUGUI text)
		{
			this.player = field.Player;
			this.text = text;
		}

		private void RefreshText ()
		{
			text.text = player.Hp.ToString ();
		}

		private void PlayerHit (object sender, System.EventArgs e)
		{
			RefreshText ();
		}

		public void Initialize ()
		{
			RefreshText ();
			player.Hit += PlayerHit;
		}
	}
}