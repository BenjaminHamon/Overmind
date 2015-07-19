using Overmind.Core.Log;
using Overmind.Games.Engine;

namespace Overmind.Games.Unity
{
	public class UnityApplication
	{
		UnityApplication()
		{
			LoggerFacade.Logger = new UnityLogger();
		}

		public readonly static UnityApplication Instance = new UnityApplication();

		public readonly Loader Loader = new Loader("../Mods");
		public string Mod;
	}
}
