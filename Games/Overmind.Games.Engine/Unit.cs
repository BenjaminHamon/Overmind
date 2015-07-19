using Overmind.Core;
using System;

namespace Overmind.Games.Engine
{
    public class Unit
	{
		public Unit(Vector position)
		{
			this.position = position;
		}

		private Vector position;
		public Vector Position { get { return position; } }

		public override string ToString()
		{
			return String.Format("[Unit Position={0}]", position);
		}

		public string ToShortString()
		{
			return "U";
		}
	}
}
