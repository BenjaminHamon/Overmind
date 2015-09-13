using Overmind.Core;
using Overmind.Games.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;

namespace Overmind.Games.Console
{
	/// <summary>
	/// Subprogram to run a game replay.
	/// Shows a game auto playing using the commands read from a file.
	/// </summary>
	public class Replay
	{
		public Replay(TextWriter output)
		{
			if (output == null)
				throw new ArgumentNullException("output", "[Replay.Constructor] Output must not be null.");
			this.output = output;
		}

		private readonly TextWriter output;
		private readonly Loader loader = new Loader(ConfigurationManager.AppSettings["ModPath"]);

		private Game game;
		private GameView gameView;
		private IList<string> commandCollection;
		private int currentCommandIndex;
		private Timer timer;

		// The shared fields isTimerRunning and timerException are written only by the timer callback and read by the main thread.
		private volatile bool isTimerRunning;
		private volatile Exception timerException;

		public void Run(IList<string> arguments)
		{
			game = loader.Load(arguments[0]);
			gameView = new GameView(game, output);

			commandCollection = File.ReadAllLines(arguments[1]);
			currentCommandIndex = 0;

			game.Start();

			isTimerRunning = true;
			TimeSpan period = TimeSpan.FromMilliseconds(Double.Parse(arguments[2]));
			timer = new Timer(_ => AutoPlay(), null, period, period);

			while (isTimerRunning)
				Thread.Sleep(1000);

			Exit();
		}

		private void AutoPlay()
		{
			if (currentCommandIndex >= commandCollection.Count)
				isTimerRunning = false;
			else
			{
				try
				{ Execute(commandCollection[currentCommandIndex]); }
				catch(Exception exception)
				{
					timerException = exception;
					isTimerRunning = false;
				}
				currentCommandIndex++;
			}
		}

		private void Execute(string command)
		{
			IList<string> arguments = command.Split(new char[] { ' ' });

			string commandName = arguments[0];
			Vector source = new Vector(Double.Parse(arguments[1]), Double.Parse(arguments[2]));
			Vector destination = new Vector(Double.Parse(arguments[3]), Double.Parse(arguments[4]));

			game.ActivePlayer.FindEntity<Entity>(source).CommandCollection.First(c => c.Name == commandName).Execute(destination);
			game.ActivePlayer.EndTurn();
		}

		private void Exit()
		{
			timer.Dispose();
			game.Dispose();

			if (timerException != null)
				throw timerException;
			else if (output == System.Console.Out)
				System.Console.Clear();
		}
	}
}
