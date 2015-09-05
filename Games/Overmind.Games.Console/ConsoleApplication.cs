using Overmind.Games.Engine;
using System.Configuration;

namespace Overmind.Games.Console
{
	public class ConsoleApplication : ViewBase
	{
		public ConsoleApplication()
			: base("App> ")
		{
			RegisterCommand("list", _ => ListMods());
			RegisterCommand("start", args => StartNewGame(args), _ => SubView == null);
		}

		private readonly Loader loader = new Loader(ConfigurationManager.AppSettings["ModPath"]);

		public override void Run()
		{
			System.Console.Title = "Overmind Games";

			base.Run();
		}

		private void ListMods()
		{
			foreach (string mod in loader.GetModList())
			{
				WriteMessage(mod);
			}
		}

		private void StartNewGame(string[] args)
		{
			GameView game = new GameView(loader.Load(args[0]));
			game.Start();
			RunSubView(game);
		}
	}
}
