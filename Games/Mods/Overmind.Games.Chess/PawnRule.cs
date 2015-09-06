using Overmind.Games.Engine;
using Overmind.Core;
using System;

namespace Overmind.Games.Chess
{
	public class PawnRule : IRule
	{
		public PawnRule(bool goUp)
		{
			VerticalDirection = goUp ? 1 : -1;
			StartingRow = goUp ? 2 : 7;
		}

		private readonly double VerticalDirection;
		private readonly double StartingRow;

		private bool IsWithinLimits(Vector vector)
		{
			return (vector[0] > 0) && (vector[0] <= 8) && (vector[1] > 0) && (vector[1] <= 8);
		}

		public bool CanMove(Vector source, Vector destination)
		{
			if (IsWithinLimits(destination) == false)
				return false;
			Vector move = destination - source;
			return (move[0] == 0) && ((move[1] == VerticalDirection) || ((source[1] == StartingRow) && (move[1] == VerticalDirection * 2)));
		}

		public bool CanTake(Vector source, Vector target)
		{
			if (IsWithinLimits(target) == false)
				return false;
			Vector move = target - source;
			return (Math.Abs(move[0]) == 1) && (move[1] == VerticalDirection);
		}

		public bool RequirePathCheck { get { return true; } }
	}
}
