using Overmind.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Overmind.Games.Console
{
	/// <summary>
	/// Base class for a command interpreter user interface.
	/// It reads commands from an input, parses and executes them.
	/// </summary>
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

		/// <summary>Initializes members according to the shell invocation arguments, before the main loop starts.</summary>
		/// <param name="arguments">Argument line provided with the shell invocation in the parent program or shell.</param>
		protected virtual void Initialize(IList<string> arguments) { }

		/// <summary>Starts and runs the shell, waiting for commands and executing them, until it exits.</summary>
		/// <param name="arguments">Argument line provided with the shell invocation in the parent program or shell.</param>
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

		/// <summary>
		/// Disposes resources and stops the shell execution.
		/// </summary>
		protected virtual void Exit()
		{
			isRunning = false;
		}

		private void Help()
		{
			IEnumerable<string> commands =  commandInterpreter.CommandNames.OrderBy(c => c);
			output.WriteLine(String.Join(" ", commands.ToArray()));
		}

		/// <summary>Helper to read input with a message explaining what is expected.</summary>
		/// <param name="message">Optional message explaining what kind of input is expected.</param>
		/// <returns>The line read from the input reader.</returns>
		protected string Read(string message = null)
		{
			if (String.IsNullOrEmpty(message) == false)
				output.Write(message + ": ");
			return input.ReadLine();
		}
	}
}
