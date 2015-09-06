using Overmind.Games.Engine;
using Overmind.Core;
using System;

namespace Overmind.Games.Chess
{
	public class BishopRule : IRule
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
			return Math.Abs(move[0]) == Math.Abs(move[1]);
		}

		public bool CanTake(Vector source, Vector target)
		{
			return CanMove(source, target);
		}

		public bool RequirePathCheck { get { return true; } }
	}
}
