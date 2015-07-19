using Overmind.Core;

namespace Overmind.Games.Engine
{
	public interface IRule
	{
		bool CanMove(Vector source, Vector destination);
		bool CanEat(Vector source, Vector target, out Vector destination);
	}
}
