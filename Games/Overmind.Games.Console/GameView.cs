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

			RegisterCommand("play", _ => Play());

			game.TurnStarted += OnTurnStarted;
		}

		private readonly Game game;

		public void Start()
		{
			game.Start();
		}

		private void OnTurnStarted(Game sender)
		{
			Draw();
		}

		private Vector ReadVector(string message)
		{
			string[] arguments = SplitArguments(Read(message));
			return new Vector(Double.Parse(arguments[0]), Double.Parse(arguments[1]));
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
		}

		private Timer autoPlayTimer;

		private void StartAutoPlay()
		{
			throw new NotImplementedException();

			if (autoPlayTimer != null)
				throw new Exception("Already running");

			autoPlayTimer = new Timer(AutoPlay, null, 0, 1000);
		}

		private void AutoPlay(object state)
		{
			try
			{
				//NextTurn();
			}
			catch (Exception exception)
			{
				if (autoPlayTimer != null)
				{
					System.Console.Error.WriteLine(exception);
					autoPlayTimer.Dispose();
					autoPlayTimer = null;
				}
			}
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
			GridView<Entity> grid = new GridView<Entity>(System.Console.Out);

			grid.PreWrite = entity => System.Console.ForegroundColor = entity.Owner.Color.Value.ToConsoleColor();
			grid.PostWrite = entity => System.Console.ResetColor();

			grid.Draw(1, game.BoardSize, 1, game.BoardSize, game.AllEntities, p => p.Position, p => p.ToShortString());
		}

		protected override void Exit()
		{
			game.Dispose();

			base.Exit();
		}
	}
}
