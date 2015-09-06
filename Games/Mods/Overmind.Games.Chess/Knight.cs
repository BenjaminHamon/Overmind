using Overmind.Games.Engine;
using Overmind.Core;

namespace Overmind.Chess
{
	public class Knight : Piece
	{
		public Knight(Player player, Vector position)
			: base(player, position, new KnightRule())
		{ }

		public override string ToShortString()
		{
			return "N";
		}
	}
}
