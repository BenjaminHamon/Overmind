using System;
using System.Drawing;

namespace Overmind.Games.Console
{
	public static class ConsoleExtensions
	{
		public static ConsoleColor ToConsoleColor(this Color color)
		{
			if (color == Color.Black)
				return ConsoleColor.Red;
			if (color == Color.White)
				return ConsoleColor.Yellow;
			throw new Exception("Cannot convert " + color + " to ConsoleColor");
		}
	}
}
