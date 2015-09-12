using Overmind.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Overmind.Games.Console
{
	public abstract class Shell
	{
		protected Shell()
		{
			commandInterpreter.RegisterCommand("help", _ => Help());
			commandInterpreter.RegisterCommand("exit", _ => Exit());
		}

		protected readonly CommandInterpreter commandInterpreter = new CommandInterpreter();

		private bool isRunning = false;

		protected TextReader input = System.Console.In;
		protected TextWriter output = System.Console.Out;
		protected TextWriter errorOutput = System.Console.Out;

		private string prompt = "> ";
		private string separator = Environment.NewLine;

		protected virtual void Initialize(IList<string> arguments) { }

		public void Run(IList<string> arguments)
		{
			isRunning = true;
			Initialize(arguments);

			while (isRunning)
			{
				output.Write(prompt);
				try
				{ commandInterpreter.ExecuteCommand(input.ReadLine()); }
				catch (Exception exception)
				{
					System.Console.ForegroundColor = ConsoleColor.Red;
					errorOutput.WriteLine(exception);
					if (isRunning == false)
					{
						errorOutput.WriteLine("Error while exiting, press any key to end");
						input.Read();
					}
					System.Console.ResetColor();
				}
				output.Write(separator);

			}
		}

		protected virtual void Exit()
		{
			isRunning = false;
		}

		private void Help()
		{
			IEnumerable<string> commands =  commandInterpreter.CommandNames.OrderBy(c => c);
			output.WriteLine(String.Join(" ", commands.ToArray()));
		}

		protected string Read(string message = null)
		{
			if (String.IsNullOrEmpty(message) == false)
				output.Write(message + ": ");
			return input.ReadLine();
		}
	}
}
