using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;

namespace Overmind.Games.Chess
{
	public abstract class Piece : Entity
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

			((Player)Owner).Move(this, destination, rule.RequirePathCheck);
		}

		public void Take(Vector target)
		{
			if (rule.CanTake(Position, target) == false)
				throw new Exception("Invalid take");
			((Player)Owner).Take(this, target, rule.RequirePathCheck);
		}
	}
}
