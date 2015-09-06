using Overmind.Games.Engine;
using Overmind.Core;

namespace Overmind.Games.Chess
{
	public class Queen : Piece
	{
		public Queen(Player player, Vector position)
			: base(player, position, new QueenRule())
		{ }

		public override string ToShortString()
		{
			return "Q";
		}
	}
}
