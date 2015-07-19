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
			//System.Console.WriteLine("Welcome, commander.");
			//System.Console.Write("Enter your character name: ");
			//string characterName = System.Console.ReadLine();

			Game game = loader.Load(args[0]);

			//game.Add(new Unit(new Vector(0, 0)));
			//game.Add(new Unit(new Vector(0, 1)));
			//game.Add(new Unit(new Vector(0, 2)));

			game.Start();
			RunSubView(new GameView(game));
		}
	}
}
