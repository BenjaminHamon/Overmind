using Overmind.Games.Engine;
using Overmind.Core;

namespace Overmind.Games.Chess
{
	public class King : Piece
	{
		public King(Player player, Vector position)
			: base(player, position, new KingRule())
		{ }

		public override string ToShortString()
		{
			return "K";
		}
	}
}
