using Overmind.Games.Engine;
using Overmind.Core;

namespace Overmind.Games.Chess
{
	public class Rook : Piece
	{
		public Rook(Player player, Vector position)
			: base(player, position, new RookRule())
		{ }

		public override string ToShortString()
		{
			return "R";
		}
	}
}
