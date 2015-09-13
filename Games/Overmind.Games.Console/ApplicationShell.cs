using Overmind.Games.Engine;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Overmind.Games.Console
{
	/// <summary>Main shell for the console application itself.</summary>
	public class ApplicationShell : Shell
	{
		public ApplicationShell()
		{
			commandInterpreter.RegisterCommand("list", _ => Output.WriteLine(String.Join(" ", loader.GetModList().ToArray())));
			commandInterpreter.RegisterCommand("play", args => new GameShell().Run(args));
			commandInterpreter.RegisterCommand("replay", args => new Replay(Output).Run(args));
			commandInterpreter.RegisterCommand("simulate", args => new Simulation().Run(args));
		}

		private readonly Loader loader = new Loader(ConfigurationManager.AppSettings["ModPath"]);

		protected override void Help(IList<string> arguments)
		{
			if (arguments.Any() == false)
				base.Help(arguments);
			else
			{
				string command = arguments[0];
				string helpFilePath = Path.Combine("Help", command + ".txt");

				if (File.Exists(helpFilePath) == false)
					Output.WriteLine("No help page found for: " + command);
				else
				{
					if (Output == System.Console.Out)
						System.Console.Clear();
					Output.WriteLine(File.ReadAllText(helpFilePath));
				}
			}
		}
	}
}
