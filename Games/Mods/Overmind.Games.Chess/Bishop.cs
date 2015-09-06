using Overmind.Games.Engine;
using Overmind.Core;

namespace Overmind.Games.Chess
{
	public class Bishop : Piece
	{
		public Bishop(Player player, Vector position)
			: base(player, position, new BishopRule())
		{ }

		public override string ToShortString()
		{
			return "B";
		}
	}
}
