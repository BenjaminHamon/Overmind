﻿using Overmind.Games.Engine;
using Overmind.Core;
using System;

namespace Overmind.Chess
{
	public class KnightRule : IRule
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
			return ((Math.Abs(move[0]) == 1) && (Math.Abs(move[1]) == 2)) || ((Math.Abs(move[0]) == 2) && (Math.Abs(move[1]) == 1));
		}

		public bool CanEat(Vector source, Vector target, out Vector destination)
		{
			throw new NotImplementedException();
		}
	}
}