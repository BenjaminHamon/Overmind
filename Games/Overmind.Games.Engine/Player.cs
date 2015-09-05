using Overmind.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Overmind.Games.Engine
{
	public class Player : IDisposable
	{
		public Player(Game game, string name, Color? color = null)
		{
			this.game = game;
			this.name = name;
			this.Color = color;
		}

		protected readonly Game game;
		private readonly string name;
		public readonly Color? Color;
		public string Name { get { return name; } }
		public int Score { get; protected set; }

		public IDictionary<string, int> ResourceCollection;

		public IStrategy Strategy { get; set; }
		public IEnumerable<ICommand> CommandCollection;

		public TEntity FindEntity<TEntity>(Vector position)
			where TEntity : Piece
		{
			return PieceCollection.OfType<TEntity>().FirstOrDefault(p => p.Position == position);
		}

		public IList<Piece> PieceCollection;

		public event Action<Player, Piece> EntityAdded;

		public void AddEntity(Piece entity)
		{
			PieceCollection.Add(entity);
			if (EntityAdded != null)
				EntityAdded(this, entity);
		}

		public override string ToString()
		{
			return String.Format("Player {0}", name);
		}

		protected virtual void Update()
		{
			//if (Strategy != null)
			//	Strategy.Update();
		}

		public void EndTurn()
		{
			Update();
			game.EndTurn();
		}

		public void Dispose()
		{
			if (Strategy is IDisposable)
				((IDisposable)Strategy).Dispose();
		}
	}
}
