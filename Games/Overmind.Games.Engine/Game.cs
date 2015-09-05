using Overmind.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overmind.Games.Engine
{
	public class Game : IDisposable
	{
		public Game(int boardSize)
		{
			this.BoardSize = boardSize;
		}

		public readonly int BoardSize;

		public void Start()
		{
			Turn = 1;
			ActivePlayer = playerCollection.First();
			if (TurnStarted != null)
				TurnStarted();
		}

		public void AddPlayer(Player player)
		{
			playerCollection.Add(player);
		}

		private readonly List<Player> playerCollection = new List<Player>();
		public IEnumerable<Player> PlayerCollection { get { return playerCollection.AsReadOnly(); } }
		public Player ActivePlayer { get; private set; }

		public TEntity FindEntity<TEntity>(Vector position)
			where TEntity : Piece
		{
			foreach (Player player in PlayerCollection)
			{
				TEntity entity = player.FindEntity<TEntity>(position);
				if (entity != null)
					return entity;
			}

			return null;
		}

		public IEnumerable<Piece> AllPieces
		{
			get 
			{
				return playerCollection.Select(player => player.PieceCollection)
					.Cast<IEnumerable<Piece>>().Aggregate((first, second) => first.Concat(second));
			}
		}

		public int Turn { get; private set; }

		public void EndTurn()
		{
			Turn++;
			ActivePlayer = playerCollection.ElementAtOrDefault(playerCollection.IndexOf(ActivePlayer) + 1) ?? playerCollection.First();
			if (TurnStarted != null)
				TurnStarted();
		}

		public event Action TurnStarted;

		public void Dispose()
		{
			foreach (Player player in playerCollection)
				player.Dispose();
		}
	}
}
