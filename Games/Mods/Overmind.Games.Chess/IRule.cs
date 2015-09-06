using Overmind.Core;

namespace Overmind.Games.Chess
{
	public interface IRule
	{
		bool CanMove(Vector source, Vector destination);
		bool CanTake(Vector source, Vector target);
		bool RequirePathCheck { get; }
	}
}
