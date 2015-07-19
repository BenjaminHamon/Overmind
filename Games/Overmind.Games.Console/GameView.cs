using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Overmind.Games.Console
{
	public class GameView : ViewBase
	{
		public GameView(Game game)
			: base("Game> ")
		{
			this.game = game;

			//RegisterCommand("show", _ => DrawGrid());
			//RegisterCommand("list", _ => List());

			RegisterCommand("move", _ => Move());
			RegisterCommand("take", _ => Take());

			// Let mod register register actions ?

			RegisterCommand("next", _ => NextTurn());
			RegisterCommand("play", _ => Play());

			Draw();
		}

		private readonly Game game;

		private Vector ReadVector(string message)
		{
			string[] arguments = SplitArguments(Read(message));
			return new Vector(Double.Parse(arguments[0]), Double.Parse(arguments[1]));
		}

		private void Move()
		{
			Vector source = ReadVector("Source");
			Vector destination = ReadVector("Destination");
			game.ActivePlayer.Move(source, destination);

			NextTurn();
		}

		private void Take()
		{
			Vector source = ReadVector("Source");
			Vector target = ReadVector("Target");
			game.ActivePlayer.Take(source, target);

			NextTurn();
		}

		private Timer playTimer;

		private void Play()
		{
			if (playTimer != null)
				throw new Exception("Already running");

			playTimer = new Timer(PlayTimerCallback, null, 0, 1000);
		}

		private void PlayTimerCallback(object state)
		{
			try
			{
				NextTurn();
			}
			catch (Exception exception)
			{
				if (playTimer != null)
				{
					System.Console.Error.WriteLine(exception);
					playTimer.Dispose();
					playTimer = null;
				}
			}
		}

		private void NextTurn()
		{
			game.NextTurn();
			Draw();
		}

		private void Draw()
		{
			System.Console.Clear();

			WriteNewLine();

			DrawGrid();

			WriteNewLine();
			WriteMessage("Turn " + game.Turn);
			WriteMessage(game.ActivePlayer + " is playing");
		}

		private void DrawGrid()
		{
			GridView<Piece> grid = new GridView<Piece>(System.Console.Out);

			grid.PreWrite = piece =>
			{
				if (game.ActivePlayer.PieceCollection.Contains(piece))
					System.Console.ForegroundColor = ConsoleColor.Yellow;
				else
					System.Console.ForegroundColor = ConsoleColor.Red;
			};

			grid.PostWrite = piece => System.Console.ResetColor();

			grid.Draw(1, game.BoardSize, 1, game.BoardSize, game.AllPieces, p => p.Position, p => p.ToShortString());
		}

		protected override void Exit()
		{
			game.Dispose();

			base.Exit();
		}
	}
}
