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
				TurnStarted(this);
		}

		public void AddPlayer(Player player)
		{
			playerCollection.Add(player);
		}

		private readonly List<Player> playerCollection = new List<Player>();
		public IEnumerable<Player> PlayerCollection { get { return playerCollection.AsReadOnly(); } }
		public Player ActivePlayer { get; private set; }

		public TEntity FindEntity<TEntity>(Vector position)
			where TEntity : Entity
		{
			foreach (Player player in PlayerCollection)
			{
				TEntity entity = player.FindEntity<TEntity>(position);
				if (entity != null)
					return entity;
			}

			return null;
		}

		public IEnumerable<Entity> AllEntities
		{
			get 
			{
				return playerCollection.Select(player => player.EntityCollection)
					.Cast<IEnumerable<Entity>>().Aggregate((first, second) => first.Concat(second));
			}
		}

		public int Turn { get; private set; }

		public void EndTurn()
		{
			Turn++;
			ActivePlayer = playerCollection.ElementAtOrDefault(playerCollection.IndexOf(ActivePlayer) + 1) ?? playerCollection.First();
			if (TurnStarted != null)
				TurnStarted(this);
		}

		public event Core.EventHandler<Game> TurnStarted;

		public void Dispose()
		{
			foreach (Player player in playerCollection)
				player.Dispose();
		}
	}
}
