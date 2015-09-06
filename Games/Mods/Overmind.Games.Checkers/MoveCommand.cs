using Overmind.Core;
using Overmind.Games.Engine;

namespace Overmind.Games.Checkers
{
	public class MoveCommand : ICommand
	{
		public MoveCommand(Piece owner)
		{
			this.owner = owner;
		}

		private readonly Piece owner;
		public string Name { get { return "Move"; } }

		public void Execute(Vector position)
		{
			owner.Move(position);
		}
	}
}
