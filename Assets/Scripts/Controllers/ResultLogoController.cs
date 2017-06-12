using Zenject;
using UnityEngine;
using UnityEngine.UI;
using AssetBundles;

namespace Dust.Controllers {
	public class ResultLogoController : IInitializable
	{
		public const string VictorySprite = "VictorySprite";
		public const string LossSprite = "LossSprite";

		private string victorSpriteName;
		private string lossSpriteName;
		private Image logo;
		private GameResult gameResult;

		private ResultLogoController (
			[Inject (Id = VictorySprite)] string victorSpriteName,
			[Inject (Id = LossSprite)] string lossSpriteName,
			Image logo,
			GameResult gameResult)
		{
			this.victorSpriteName = victorSpriteName;
			this.lossSpriteName = lossSpriteName;
			this.logo = logo;
			this.gameResult = gameResult;
		}

		public void Initialize ()
		{
			string spriteName = null;
			switch (gameResult)
			{
			case GameResult.Victory:
				spriteName = victorSpriteName;
				break;

			case GameResult.Loss:
				spriteName = lossSpriteName;
				break;
			}

			Sprite sprite = AssetBundleManager.LoadAsset<Sprite> (
				AssetBundleNames.GameplayBundle, spriteName);
			logo.sprite = sprite;
		}
	}
}