using Overmind.Core;
using Overmind.Games.Engine;

namespace Overmind.Games.Chess
{
	public class TakeCommand : ICommand
	{
		public TakeCommand(Piece owner)
		{
			this.owner = owner;
		}

		private readonly Piece owner;
		public string Name { get { return "Take"; } }

		public void Execute(Vector position)
		{
			owner.Take(position);
		}
	}
}
