using Overmind.Games.Engine;
using Overmind.Core;
using System;

namespace Overmind.Chess
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

		public bool CanEat(Vector source, Vector target, out Vector destination)
		{
			throw new NotImplementedException();

			destination = null;
			Vector move = (target - source) * 2;
			if ((Math.Abs(move[0]) != 2) || (Math.Abs(move[1]) != 2))
				return false;
			destination = source + move;
			return IsWithinLimits(destination);
		}
	}
}
