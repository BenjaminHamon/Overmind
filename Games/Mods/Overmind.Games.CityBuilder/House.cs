using Overmind.Core;
using Overmind.Games.Engine;

namespace Overmind.Games.CityBuilder
{
	public class House : Entity
	{
		public House(Player owner, Vector position)
			: base(owner, position)
		{ }

		public override string ToShortString() { return "H"; }
	}
}
