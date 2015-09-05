using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;

namespace Overmind.Chess
{
	public class Piece : Overmind.Games.Engine.Piece
	{
		public Piece(Player owner, Vector position, IRule rule)
			: base(owner, position)
		{
			this.rule = rule;

			CommandCollection = new List<ICommand>()
			{
				new MoveCommand(this),
				new TakeCommand(this),
			};
		}

		private readonly IRule rule;

		public void Move(Vector destination)
		{
			if (rule.CanMove(Position, destination) == false)
				throw new Exception("Invalid move");

			((Player)Owner).Move(this, destination);
		}

		public void Take(Vector targetPosition)
		{
			Vector destination;
			if (rule.CanTake(Position, targetPosition, out destination) == false) // FIXME: NO need for destination in Chess
				throw new Exception("Invalid take");
			((Player)Owner).Take(this, targetPosition, destination);
		}
	}
}
