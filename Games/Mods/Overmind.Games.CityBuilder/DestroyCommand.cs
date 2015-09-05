using Overmind.Core;
using Overmind.Games.Engine;

namespace Overmind.Games.CityBuilder
{
	public class DestroyCommand : ICommand
	{
		public DestroyCommand(Player owner)
		{
			this.owner = owner;
		}

		private readonly Player owner;
		public string Name { get { return "Destroy"; } }

		public void Execute(Vector position)
		{
			owner.Destroy(position);
		}
	}
}
