using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Overmind.Games.Console
{
	/// <summary>Console user interface for a game.</summary>
	public class GameShell : Shell
	{
		public GameShell()
		{
			commandInterpreter.RegisterCommand("clear", _ => Draw());
			commandInterpreter.RegisterCommand("play", _ => Play());
		}

		private readonly Loader loader = new Loader(ConfigurationManager.AppSettings["ModPath"]);
		private Game game;

		protected override void Initialize(IList<string> arguments)
		{
			game = loader.Load(arguments.First());
			game.TurnStarted += _ => Draw();
			game.Start();
		}

		private void Play()
		{
			Vector source = ReadVector("Source");
			Entity entity = game.ActivePlayer.FindEntity<Entity>(source);
			if (entity == null)
				throw new Exception("No entity found");

			string commandName = Read("Command");
			ICommand command = entity.CommandCollection.First(c => c.Name == commandName);

			Vector destination = ReadVector("Destination");
			command.Execute(destination);
			game.ActivePlayer.EndTurn();
		}

		private Vector ReadVector(string message)
		{
			IList<string> arguments = Read(message).Split(new char[] { ' ' });
			return new Vector(Double.Parse(arguments[0]), Double.Parse(arguments[1]));
		}

		private void Draw()
		{
			if (output == System.Console.Out)
				System.Console.Clear();

			output.WriteLine();

			DrawGrid();

			output.WriteLine();
			output.WriteLine("Turn " + game.Turn);
			output.WriteLine(game.ActivePlayer + " is playing");
		}

		private void DrawGrid()
		{
			GridView<Entity> grid = new GridView<Entity>(output);

			if (output == System.Console.Out)
			{
				grid.PreWrite = entity => System.Console.ForegroundColor = entity.Owner.Color.Value.ToConsoleColor();
				grid.PostWrite = entity => System.Console.ResetColor();
			}

			grid.Draw(1, game.BoardSize, 1, game.BoardSize, game.AllEntities, p => p.Position, p => p.ToShortString());
		}

		protected override void Exit()
		{
			game.Dispose();
			if (output == System.Console.Out)
				System.Console.Clear();

			base.Exit();
		}
	}
}
