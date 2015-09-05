using Overmind.Core;
using Overmind.Games.Engine;
using Overmind.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class GameView : MonoBehaviourBase
	{
		public string Mod;

		private Game game;
		private ContentLoader contentLoader;
		
		public override void Start()
		{
			if (UnityApplication.Instance.Mod != null)
				Mod = UnityApplication.Instance.Mod;

			game = UnityApplication.Instance.Loader.Load(Mod);

			contentLoader = new ContentLoader("file:///E:/Projects/Overmind/Games/Mods");
			DependencyReadyStatus.Add(contentLoader, false);
			contentLoader.Ready += OnDependencyReady;
			StartCoroutine(contentLoader.LoadAssetBundleAsync(Mod));
		}

		private readonly IDictionary<object, bool> DependencyReadyStatus = new Dictionary<object, bool>();

		private void OnDependencyReady(IDependency dependency)
		{
			dependency.Ready -= OnDependencyReady;
			DependencyReadyStatus[dependency] = true;
			if (DependencyReadyStatus.Values.All(status => status))
				DoStart();
		}

		private void DoStart()
		{
			BoardSize = game.BoardSize;
			for (int cellIndex = 0; cellIndex < BoardSize * BoardSize; cellIndex++)
			{
				GameObject cell = Instantiate(CellPrefab);
				cell.transform.SetParent(transform);
				CellView view = cell.GetComponent<CellView>();
				view.Position = new Vector((cellIndex % BoardSize) + 1, (cellIndex / BoardSize) + 1);
				view.name = "Cell " + view.Position;
				view.Clicked += OnCellClicked;
			}

			foreach (Player player in game.PlayerCollection)
			{
				PlayerView playerView = Instantiate<GameObject>(PlayerViewPrefab).GetComponent<PlayerView>();
				playerView.gameObject.SetActive(false);
				playerView.Initialize(player);
				playerView.transform.SetParent(PlayerViewGroup, false);
				playerViewCollection.Add(playerView);

				foreach (Piece entity in player.PieceCollection)
					CreateEntityView(player, entity);

				player.EntityAdded += CreateEntityView;
				playerView.Selection.Changed += OnSelectionChanged;
			}

			game.TurnStarted += OnTurnStarted;
			game.Start();
		}

		private readonly IList<PlayerView> playerViewCollection = new List<PlayerView>();
		private PlayerView activePlayerView;
		public Transform PlayerViewGroup;
		public GameObject PlayerViewPrefab;

		private void CreateEntityView(Player player, Piece entity)
		{
			PieceView entityView = Instantiate(PiecePrefab).GetComponent<PieceView>();
			entityView.Initialize(this, playerViewCollection.First(playerView => playerView.Player == player), entity, contentLoader.GetAssetBundle(Mod));
			entityView.transform.SetParent(GetCell(entity.Position).transform, false);
		}

		public GridLayoutGroup Grid;
		public GameObject CellPrefab;
		public GameObject PiecePrefab;

		[SerializeField, HideInInspector]
		private int boardSize = 10;

		[ExposeProperty]
		public int BoardSize
		{
			get { return boardSize; }
			set
			{
				if ((value <= 0) || (boardSize == value))
					return;
				boardSize = value;
				RectTransform transform = (RectTransform)Grid.transform;
				Grid.cellSize = new Vector2(transform.rect.width / boardSize, transform.rect.height / boardSize);
				Grid.constraintCount = boardSize;
			}
		}

		public Text TurnText;
		public EntityInfoView EntityInfo;

		private void OnTurnStarted()
		{
			if (activePlayerView != null)
				activePlayerView.gameObject.SetActive(false);

			activePlayerView = playerViewCollection.First(playerView => playerView.Player == game.ActivePlayer);
			activePlayerView.gameObject.SetActive(true);

			TurnText.text = "Turn " + game.Turn;
		}

		private void OnSelectionChanged(Selection<CellView> sender)
		{
			EntityInfo.SetEntity(sender.Item != null ? sender.Item.GetComponentInChildren<PieceView>() : null, activePlayerView);
		}

		private void OnCellClicked(CellView sender)
		{
			activePlayerView.OnCellClick(sender);
		}

		public CellView GetCell(Vector vector)
		{
			int cellIndex = Convert.ToInt32(vector[0] - 1 + (vector[1] - 1) * BoardSize);
			return transform.GetChild(cellIndex).GetComponent<CellView>();
		}
	}
}
