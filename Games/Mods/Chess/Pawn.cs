using Overmind.Games.Engine;
using Overmind.Core;

namespace Overmind.Chess
{
	public class Pawn : Piece
	{
		public Pawn(Player player, Vector position, bool goUp)
			: base(player, position, new PawnRule(goUp))
		{ }

		public override string ToShortString()
		{
			return "P";
		}
	}
}
