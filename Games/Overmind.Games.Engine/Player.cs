using Overmind.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Overmind.Games.Engine
{
	public class Player : IDisposable
	{
		public Player(Game game, string name, Color color)
		{
			this.game = game;
			this.name = name;
			this.Color = color;
		}

		private readonly Game game;
		private readonly string name;
		public readonly Color Color;
		public string Name { get { return name; } }

		public IStrategy Strategy { get; set; }

		public void Move(Vector source, Vector destination)
		{
			Piece piece = PieceCollection.FirstOrDefault(p => p.Position == source);
			if (piece == null)
				throw new Exception("Source piece not found");

			if (FindPiece(destination) != null)
				throw new Exception("Destination is occupied");

			piece.Move(destination);
		}

		public void Take(Vector source, Vector target)
		{
			Piece sourcePiece = PieceCollection.FirstOrDefault(p => p.Position == source);
			if (sourcePiece == null)
				throw new Exception("Source piece not found");

			Piece targetPiece = FindPiece(target);
			if (targetPiece == null)
				throw new Exception("Target piece not found");
			if (PieceCollection.Contains(targetPiece))
				throw new Exception("Target piece belongs to the active player");

			Vector destination = sourcePiece.Take(targetPiece);
			if (FindPiece(destination) != null)
				throw new Exception("Destination is occupied");

			targetPiece.Destroy();
			sourcePiece.DoMove(destination);
		}

		private Piece FindPiece(Vector position)
		{
			foreach (Player player in game.PlayerCollection)
			{
				Piece piece = player.PieceCollection.FirstOrDefault(p => p.Position == position);
				if (piece != null)
					return piece;
			}

			return null;
		}
		
		public IList<Piece> PieceCollection;

		public Piece GetPiece(Vector position)
		{
			return PieceCollection.FirstOrDefault(p => p.Position == position);
		}

		public override string ToString()
		{
			return String.Format("Player {0}", name);
		}

		internal void Update()
		{
			if (Strategy != null)
				Strategy.Update();
		}

		public void Dispose()
		{
			if (Strategy is IDisposable)
				((IDisposable)Strategy).Dispose();
		}
	}
}
