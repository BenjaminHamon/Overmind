using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Drawing;

namespace Overmind.Games.Chess
{
	public class Player : Overmind.Games.Engine.Player
	{
		public Player(Game game, string name, Color color)
			: base(game, name, color)
		{ }

		public void Move(Piece source, Vector destination)
		{
			if (game.FindEntity<Piece>(destination) != null)
				throw new Exception("Destination is occupied");

			source.Position = destination;

			InternalEndTurn();
		}

		public void Take(Piece source, Vector target, Vector destination)
		{
			Piece targetPiece = game.FindEntity<Piece>(target);
			if (targetPiece == null)
				throw new Exception("Target piece not found");
			if (EntityCollection.Contains(targetPiece))
				throw new Exception("Target piece belongs to the active player");
			if (game.FindEntity<Piece>(destination) != null)
				throw new Exception("Destination is occupied");

			targetPiece.Destroy();
			source.Position = destination;

			InternalEndTurn();
		}

		public override bool CanEndTurn { get { return false; } }
	}
}
