using Overmind.Games.Engine;
using Overmind.Core;
using System;

namespace Overmind.Chess
{
	public class RookRule : IRule
	{
		private bool IsWithinLimits(Vector vector)
		{
			return (vector[0] > 0) && (vector[0] <= 8) && (vector[1] > 0) && (vector[1] <= 8);
		}

		public bool CanMove(Vector source, Vector destination)
		{
			if (IsWithinLimits(destination) == false)
				return false;
			Vector move = destination - source;
			return (move[0] == 0) || (move[1] == 0);
		}

		public bool CanTake(Vector source, Vector target, out Vector destination)
		{
			throw new NotImplementedException();
		}
	}
}
