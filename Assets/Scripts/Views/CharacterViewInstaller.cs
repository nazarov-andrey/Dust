﻿using Zenject;
using UnityEngine.UI;
using UnityEngine;
using Dust.Views.Animations;
using System.IO;
using AssetBundles;

namespace Dust.Views {
	public class CharacterViewInstaller : MonoInstaller
	{
		private string kind;

		[Inject]
		private void Inject (string kind)
		{
			this.kind = kind;
		}

		public override void InstallBindings ()
		{
			Container
				.Bind<CharacterView> ()
				.AsSingle ();

			Container
				.Bind<ICharacterAnimation> ()
				.To<AnimatorCharacterAnimation> ()
				.AsSingle ();

			Container
				.Bind<RectTransform> ()
				.FromMethod (x => GetComponent<RectTransform> ());

			Container
				.Bind<Animator> ()
				.FromComponentInNewPrefab (
					AssetBundleManager.LoadAsset<GameObject> (AssetBundleNames.GameplayBundle, kind))
				.AsSingle ();
		}
	}
}