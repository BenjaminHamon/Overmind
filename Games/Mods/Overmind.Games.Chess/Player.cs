using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Drawing;
using System.Linq;

namespace Overmind.Games.Chess
{
	public class Player : Overmind.Games.Engine.Player
	{
		public Player(Game game, string name, Color color)
			: base(game, name, color)
		{ }

		public void Move(Piece source, Vector destination, bool checkPath)
		{
			if (game.FindEntity<Piece>(destination) != null)
				throw new Exception("Destination is occupied");
			if (checkPath && (IsPathEmpty(source.Position, destination) == false))
				throw new Exception("Path to destination is not empty");

			source.Position = destination;

			InternalEndTurn();
		}

		public void Take(Piece source, Vector target, bool checkPath)
		{
			Piece targetPiece = game.FindEntity<Piece>(target);
			if (targetPiece == null)
				throw new Exception("Target piece not found");
			if (EntityCollection.Contains(targetPiece))
				throw new Exception("Target piece belongs to the active player");
			if (checkPath && (IsPathEmpty(source.Position, target) == false))
				throw new Exception("Path to target is not empty");

			targetPiece.Destroy();
			source.Position = target;

			InternalEndTurn();
		}

		private bool IsPathEmpty(Vector source, Vector destination)
		{
			Vector move = destination - source;
			if (move == new Vector(0, 0))
				return true;

			// Normalize on each coordinate instead of using Vector.Normalize to get integers.
			Vector unitVector = new Vector(move.Select(coord => coord == 0d ? 0d : (coord > 0d ? 1d : -1d)));
			for (Vector current = source + unitVector; current != destination; current += unitVector)
				if (game.FindEntity<Piece>(current) != null)
					return false;
			return true;
		}

		public override bool CanEndTurn { get { return false; } }
	}
}
