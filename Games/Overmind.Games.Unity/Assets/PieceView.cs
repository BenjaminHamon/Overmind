using Overmind.Games.Engine;
using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class PieceView : MonoBehaviourBase
	{
		public void Initialize(GameView game, Piece piece, AssetBundle assetBundle)
		{
			this.game = game;
			this.piece = piece;
			//image.color = color;

			//image.sprite = Resources.Load<Sprite>("WhiteKnight");
			//ContentLoader loader = new ContentLoader();
			//StartCoroutine(loader.LoadAssetBundleAsync("Chess", assetBundle => image.sprite = assetBundle.LoadAsset<Sprite>("WhiteKnight")));
			image.sprite = assetBundle.LoadAsset<Sprite>(piece.GetUnitySpriteName());

			piece.Moved += OnMoved;
			piece.Destroyed += OnDestroyed;
		}

		private GameView game;
		private Piece piece;

		public Image image;

		private void OnMoved(Piece sender)
		{
			transform.SetParent(game.GetCell(piece.Position).transform, false);
		}

		private void OnDestroyed(Piece sender)
		{
			piece.Moved -= OnMoved;
			piece.Destroyed -= OnDestroyed;

			Destroy(gameObject);
		}
	}
}
