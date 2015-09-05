using System;

namespace Overmind.Games.Unity
{
	/// <summary>
	/// Interface for objects requiring asynchronous loading or setup.
	/// </summary>
	public interface IDependency
	{
		event Action<IDependency> Ready;
	}
}
