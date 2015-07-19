using Overmind.Games.Engine;
using Overmind.Core;
using System;

namespace Overmind.Checkers
{
	public class CheckerRule : IRule
	{
		// Several moves in a row
		// Mandatory take

		public CheckerRule(bool goUp)
		{
			this.VerticalDirection = goUp ? 1 : -1;
		}

		private readonly double VerticalDirection;

		private bool IsWithinLimits(Vector vector)
		{
			return (vector[0] > 0) && (vector[0] <= 10) && (vector[1] > 0) && (vector[1] <= 10);
		}

		public bool CanMove(Vector source, Vector destination)
		{
			if (IsWithinLimits(destination) == false)
				return false;
			Vector move = destination - source;
			return (Math.Abs(move[0]) == 1) && (move[1] == VerticalDirection);
		}

		public bool CanEat(Vector source, Vector target, out Vector destination)
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
