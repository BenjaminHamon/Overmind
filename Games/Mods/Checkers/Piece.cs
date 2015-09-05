using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;

namespace Overmind.Checkers
{
	public class Piece : Entity
	{
		// Several moves in a row
		// Mandatory take

		public Piece(Player owner, Vector position, bool goUp)
			: base(owner, position)
		{
			CommandCollection = new List<ICommand>()
			{
				new MoveCommand(this),
				new TakeCommand(this),
			};

			this.VerticalDirection = goUp ? 1 : -1;
		}

		public override string ToShortString() { return "C"; }

		private readonly double VerticalDirection;

		private bool IsWithinLimits(Vector vector)
		{
			return (vector[0] > 0) && (vector[0] <= Main.BoardSize) && (vector[1] > 0) && (vector[1] <= Main.BoardSize);
		}

		public void Move(Vector destination)
		{
			if (CanMove(Position, destination) == false)
				throw new Exception("Invalid move");

			((Player)Owner).Move(this, destination);
		}

		private bool CanMove(Vector source, Vector destination)
		{
			if (IsWithinLimits(destination) == false)
				return false;
			Vector move = destination - source;
			return (Math.Abs(move[0]) == 1) && (move[1] == VerticalDirection);
		}

		public void Take(Vector target)
		{
			Vector destination;
			if (CanTake(Position, target, out destination) == false)
				throw new Exception("Invalid take");
			((Player)Owner).Take(this, target, destination);
		}

		private bool CanTake(Vector source, Vector target, out Vector destination)
		{
			destination = null;
			Vector move = (target - source) * 2;
			if ((Math.Abs(move[0]) != 2) || (Math.Abs(move[1]) != 2))
				return false;
			destination = source + move;
			return IsWithinLimits(destination);
		}
	}
}
