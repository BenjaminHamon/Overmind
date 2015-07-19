using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Overmind.Chess
{
	public class Main : IGameBuilder
	{
		private static IDictionary<Vector, Type> WhiteSetup = new Dictionary<Vector, Type>
		{
			{ new Vector(1, 1), typeof(Rook) },
			{ new Vector(2, 1), typeof(Knight) },
			{ new Vector(3, 1), typeof(Bishop) },
			{ new Vector(4, 1), typeof(Queen) },
			{ new Vector(5, 1), typeof(King) },
			{ new Vector(6, 1), typeof(Bishop) },
			{ new Vector(7, 1), typeof(Knight) },
			{ new Vector(8, 1), typeof(Rook) },
			
			{ new Vector(1, 2), typeof(Pawn) },
			{ new Vector(2, 2), typeof(Pawn) },
			{ new Vector(3, 2), typeof(Pawn) },
			{ new Vector(4, 2), typeof(Pawn) },
			{ new Vector(5, 2), typeof(Pawn) },
			{ new Vector(6, 2), typeof(Pawn) },
			{ new Vector(7, 2), typeof(Pawn) },
			{ new Vector(8, 2), typeof(Pawn) },
		};

		private static IDictionary<Vector, Type> BlackSetup = new Dictionary<Vector, Type>
		{
			{ new Vector(1, 8), typeof(Rook) },
			{ new Vector(2, 8), typeof(Knight) },
			{ new Vector(3, 8), typeof(Bishop) },
			{ new Vector(4, 8), typeof(Queen) },
			{ new Vector(5, 8), typeof(King) },
			{ new Vector(6, 8), typeof(Bishop) },
			{ new Vector(7, 8), typeof(Knight) },
			{ new Vector(8, 8), typeof(Rook) },
			
			{ new Vector(1, 7), typeof(Pawn) },
			{ new Vector(2, 7), typeof(Pawn) },
			{ new Vector(3, 7), typeof(Pawn) },
			{ new Vector(4, 7), typeof(Pawn) },
			{ new Vector(5, 7), typeof(Pawn) },
			{ new Vector(6, 7), typeof(Pawn) },
			{ new Vector(7, 7), typeof(Pawn) },
			{ new Vector(8, 7), typeof(Pawn) },
		};

		private const string ScriptDirectory = @"E:\Projects\Overmind\Games\Mods\Chess";

		public Game Create()
		{
			Game game = new Game(8);

			string filePath = Path.Combine(ScriptDirectory, String.Format("ScriptedStrategy_1.txt"));
			IEnumerable<string> actionCollection = File.ReadAllLines(filePath);

			Player player = new Player(game, "White", Color.White);
			player.PieceCollection = WhiteSetup.Select(pair =>
			{
				if (pair.Value == typeof(Pawn))
					return new Pawn(player, pair.Key, true);
				return (Piece)Activator.CreateInstance(pair.Value, player, pair.Key);
			}).ToList();
			//player.Strategy = new ScriptedStrategy(player, actionCollection.Where((_, index) => index % 2 == 0));
			game.AddPlayer(player);

			player = new Player(game, "Black", Color.Black);
			player.PieceCollection = BlackSetup.Select(pair =>
			{
				if (pair.Value == typeof(Pawn))
					return new Pawn(player, pair.Key, false);
				return (Piece)Activator.CreateInstance(pair.Value, player, pair.Key);
			}).ToList();
			//player.Strategy = new ScriptedStrategy(player, actionCollection.Where((_, index) => index % 2 == 1));
			game.AddPlayer(player);

			return game;
		}
	}
}
