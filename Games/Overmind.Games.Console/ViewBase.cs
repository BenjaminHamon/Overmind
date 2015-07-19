using Overmind.Core;
using System;

namespace Overmind.Games.Console
{
	public abstract class ViewBase : CommandInterpreter
	{
		protected ViewBase(string prompt = "> ")
		{
			this.prompt = prompt;

			RegisterCommand("exit", _ => Exit());
		}

		private bool isRunning = false;

		private readonly string prompt;
		private readonly string separator = Environment.NewLine;

		public virtual void Run()
		{
			isRunning = true;

			while (isRunning)
			{
				System.Console.Write(prompt);
				try
				{
					string input = System.Console.ReadLine();
					if (String.IsNullOrEmpty(input) == false)
						ExecuteCommand(SplitArguments(input));
				}
				catch (Exception exception)
				{
					System.Console.Error.WriteLine(exception);
					//System.Console.Error.WriteLine(exception.Message);
					if (isRunning == false)
					{
						System.Console.WriteLine("Error while exiting, press any key to end");
						System.Console.ReadKey();
					}
				}
				System.Console.Write(separator);

				if (subView != null)
					subView.Run();
			}
		}

		private ViewBase subView;
		protected ViewBase SubView { get { return subView; } }

		protected void RunSubView(ViewBase view)
		{
			subView = view;
			subView.Exited += CleanSubView;
		}

		private void CleanSubView(object sender, EventArgs args)
		{
			subView.Exited -= CleanSubView;
			subView = null;
		}

		public event ExitedEventHandler Exited;

		protected virtual void Exit()
		{
			isRunning = false;

			if (Exited != null)
				Exited(this, new EventArgs());
		}

		protected void WriteNewLine() { System.Console.WriteLine(); }
		protected void WriteMessage(string message) { System.Console.WriteLine(message); }
		protected void WriteError(Exception error) { System.Console.Error.WriteLine(error); }
		protected void WriteError(string error) { System.Console.Error.WriteLine(error); }

		protected string Read(string message = null)
		{
			if (String.IsNullOrEmpty(message) == false)
				System.Console.Write(message + ": ");
			return System.Console.ReadLine();
		}
	}

	public delegate void ExitedEventHandler(object sender, EventArgs e);
}
