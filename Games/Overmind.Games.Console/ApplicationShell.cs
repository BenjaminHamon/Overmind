using Overmind.Games.Engine;
using System;
using System.Configuration;
using System.Linq;

namespace Overmind.Games.Console
{
	public class ApplicationShell : Shell
	{
		public ApplicationShell()
		{
			commandInterpreter.RegisterCommand("list", _ => output.WriteLine(String.Join(" ", loader.GetModList().ToArray())));
			commandInterpreter.RegisterCommand("play", args => new GameShell().Run(args));
		}

		private readonly Loader loader = new Loader(ConfigurationManager.AppSettings["ModPath"]);
	}
}
