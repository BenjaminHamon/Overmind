using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Overmind.Games.Console
{
	/// <summary>
	/// Subprogram to run a game simulation.
	/// Auto play a game using the commands read from a file.
	/// </summary>
	public class Simulation
	{
		private readonly Loader loader = new Loader(ConfigurationManager.AppSettings["ModPath"]);

		private Game game;
		private IList<string> commandCollection;
		private int currentCommandIndex;

		public void Run(IList<string> arguments)
		{
			game = loader.Load(arguments[0]);
			game.TurnStarted += OnTurnStarted;

			commandCollection = File.ReadAllLines(arguments[1]);
			currentCommandIndex = 0;

			game.Start();

			game.Dispose();
		}

		private void OnTurnStarted(Game sender)
		{
			ExecuteNextCommand();
		}

		private void ExecuteNextCommand()
		{
			Execute(commandCollection[currentCommandIndex]);

			currentCommandIndex++;
			if (currentCommandIndex >= commandCollection.Count)
				game.TurnStarted -= OnTurnStarted;

			game.ActivePlayer.EndTurn();
		}

		private void Execute(string command)
		{
			IList<string> arguments = command.Split(new char[] { ' ' });

			string commandName = arguments[0];
			Vector source = new Vector(Double.Parse(arguments[1]), Double.Parse(arguments[2]));
			Vector destination = new Vector(Double.Parse(arguments[3]), Double.Parse(arguments[4]));

			game.ActivePlayer.FindEntity<Entity>(source).CommandCollection.First(c => c.Name == commandName).Execute(destination);
		}
	}
}
