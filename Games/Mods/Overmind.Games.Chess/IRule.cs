using Overmind.Core;

namespace Overmind.Chess
{
	public interface IRule
	{
		bool CanMove(Vector source, Vector destination);
		bool CanTake(Vector source, Vector target, out Vector destination);
	}
}
