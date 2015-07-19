using Overmind.Core;
using Overmind.Games.Engine;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Overmind.Checkers
{
	public class Main : IGameBuilder
	{
		private static Vector[] WhiteSetup = { 
			new Vector(1, 1), new Vector(3, 1), new Vector(5, 1), new Vector(7, 1), new Vector(9, 1),
			new Vector(2, 2), new Vector(4, 2), new Vector(6, 2), new Vector(8, 2), new Vector(10, 2),
			new Vector(1, 3), new Vector(3, 3), new Vector(5, 3), new Vector(7, 3), new Vector(9, 3),
			new Vector(2, 4), new Vector(4, 4), new Vector(6, 4), new Vector(8, 4), new Vector(10, 4),
		};

		private static Vector[] BlackSetup = { 
			new Vector(2, 10), new Vector(4, 10), new Vector(6, 10), new Vector(8, 10), new Vector(10, 10),
			new Vector(1, 9), new Vector(3, 9), new Vector(5, 9), new Vector(7, 9), new Vector(9, 9),
			new Vector(2, 8), new Vector(4, 8), new Vector(6, 8), new Vector(8, 8), new Vector(10, 8),
			new Vector(1, 7), new Vector(3, 7), new Vector(5, 7), new Vector(7, 7), new Vector(9, 7),
		};

		private const string ScriptDirectory = @"E:\Projects\Overmind\Games\Mods\Checkers";

		public Game Create()
		{
			Game game = new Game(10);

			Player player = new Player(game, "White", Color.White);
			player.PieceCollection = WhiteSetup.Select(position => new Piece(player, position, new CheckerRule(true))).ToList();
			//player.Strategy = new ScriptedStrategy(player, File.ReadAllLines(Path.Combine(ScriptDirectory, "ScriptedStrategy_White.txt")));
			game.AddPlayer(player);

			player = new Player(game, "Black", Color.Black);
			player.PieceCollection = BlackSetup.Select(position => new Piece(player, position, new CheckerRule(false))).ToList();
			//player.Strategy = new ScriptedStrategy(player, File.ReadAllLines(Path.Combine(ScriptDirectory, "ScriptedStrategy_Black.txt")));
			game.AddPlayer(player);

			return game;
		}
	}
}
