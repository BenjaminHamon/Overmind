using Overmind.Core;
using Overmind.Games.Engine;
using Overmind.Unity;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class GameView : MonoBehaviourBase
	{
		public string Mod;

		private Game game;
		
		public override void Start()
		{
			if (UnityApplication.Instance.Mod != null)
				Mod = UnityApplication.Instance.Mod;

			game = UnityApplication.Instance.Loader.Load(Mod);

			ContentLoader contentLoader = new ContentLoader();
			StartCoroutine(contentLoader.LoadAssetBundleAsync(Mod, LateStart));
		}

		private void LateStart(AssetBundle assetBundle)
		{
			BoardSize = game.BoardSize;
			for (int cellIndex = 0; cellIndex < BoardSize * BoardSize; cellIndex++)
			{
				GameObject cell = Instantiate(CellPrefab);
				cell.transform.SetParent(transform);
				CellView view = cell.GetComponent<CellView>();
				view.Position = new Vector((cellIndex % BoardSize) + 1, (cellIndex / BoardSize) + 1);
				view.name = "Cell " + view.Position;
				view.Selected += OnCellSelected;
				view.Targeted += OnCellTargeted;
			}

			foreach (Player player in game.PlayerCollection)
			{
				foreach (Piece piece in player.PieceCollection)
				{
					GameObject pieceObject = Instantiate(PiecePrefab);
					pieceObject.GetComponent<PieceView>().Initialize(this, piece, assetBundle);
					pieceObject.transform.SetParent(GetCell(piece.Position).transform);
				}
			}

			game.Start();
			ActivePlayerText.text = game.ActivePlayer.ToString();
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

		public Text ActivePlayerText;

		private CellView selection;
		private CellView Selection
		{
			get { return selection; }
			set
			{
				if (selection != null)
					selection.IsSelected = false;
				selection = value;
			}
		}

		private void OnCellSelected(CellView sender)
		{
			Selection = sender;
		}

		private void OnCellTargeted(CellView sender)
		{
			if (Selection != null)
			{
				if (sender.Piece == null)
					game.ActivePlayer.Move(Selection.Position, sender.Position);
				else
					game.ActivePlayer.Take(Selection.Position, sender.Position);
				Selection = null;
				game.NextTurn();
				ActivePlayerText.text = game.ActivePlayer.ToString();
			}
		}

		public CellView GetCell(Vector vector)
		{
			int cellIndex = Convert.ToInt32(vector[0] - 1 + (vector[1] - 1) * BoardSize);
			return transform.GetChild(cellIndex).GetComponent<CellView>();
		}
	}
}
