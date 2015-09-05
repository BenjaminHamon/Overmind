using Overmind.Games.Engine;
using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class PieceView : MonoBehaviourBase
	{
		public void Initialize(GameView game, PlayerView owner, Piece entity, AssetBundle assetBundle)
		{
			this.game = game;
			this.Owner = owner;
			this.Entity = entity;

			name = entity.GetType().Name;
			//image.color = color;

			//image.sprite = Resources.Load<Sprite>("WhiteKnight");
			//ContentLoader loader = new ContentLoader();
			//StartCoroutine(loader.LoadAssetBundleAsync("Chess", assetBundle => image.sprite = assetBundle.LoadAsset<Sprite>("WhiteKnight")));
			image.sprite = assetBundle.LoadAsset<Sprite>(entity.Image);

			entity.Moved += OnMoved;
			entity.Destroyed += OnDestroyed;
		}

		private GameView game;
		public PlayerView Owner { get; private set; }
		public Piece Entity { get; private set; }

		public Image image;

		private void OnMoved(Piece sender)
		{
			transform.SetParent(game.GetCell(Entity.Position).transform, false);
		}

		private void OnDestroyed(Piece sender)
		{
			Entity.Moved -= OnMoved;
			Entity.Destroyed -= OnDestroyed;

			Destroy(gameObject);
		}
	}
}
