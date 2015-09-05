using Overmind.Core;

namespace Overmind.Games.Engine
{
	public interface ICommand
	{
		string Name { get; }
		void Execute(Vector position);
	}
}
