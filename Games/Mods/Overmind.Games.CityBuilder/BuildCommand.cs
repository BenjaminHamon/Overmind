using Overmind.Core;
using Overmind.Games.Engine;

namespace Overmind.Games.CityBuilder
{
	public class BuildCommand : ICommand
	{
		public BuildCommand(Player owner)
		{
			this.owner = owner;
		}

		private readonly Player owner;
		public string Name { get { return "Build"; } }

		public void Execute(Vector position)
		{
			owner.Build(position);
		}
	}
}
