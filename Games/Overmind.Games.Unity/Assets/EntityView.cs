using Overmind.Games.Engine;
using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class EntityView : MonoBehaviourBase
	{
		public void Initialize(GameView game, PlayerView owner, Entity entity, AssetBundle assetBundle)
		{
			this.game = game;
			this.Owner = owner;
			this.Entity = entity;

			name = entity.GetType().Name;
			image.sprite = assetBundle.LoadAsset<Sprite>(entity.Image);

			entity.Moved += OnMoved;
			entity.Destroyed += OnDestroyed;
		}

		private GameView game;
		public PlayerView Owner { get; private set; }
		public Entity Entity { get; private set; }

		[SerializeField]
		private Image image;

		private void OnMoved(Entity sender)
		{
			transform.SetParent(game.GetCell(Entity.Position).transform, false);
		}

		private void OnDestroyed(Entity sender)
		{
			Entity.Moved -= OnMoved;
			Entity.Destroyed -= OnDestroyed;

			Destroy(gameObject);
		}
	}
}
