using Overmind.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Overmind.Games.Engine
{
	public class ScriptedStrategy : IStrategy
	{
		private const string ScriptDirectory = @"E:\Projects\Overmind\Games\Mods\Chess";

		public ScriptedStrategy(Player owner, IEnumerable<string> actionCollection)
		{
			this.owner = owner;
			this.actionCollection = actionCollection;
		}

		private readonly Player owner;
		private readonly IEnumerable<string> actionCollection;
		private int actionIndex = 0;

		private char[] Separators = { ' ' };

		public void Update()
		{
			Queue<string> action = new Queue<string>(actionCollection.ElementAt(actionIndex).Split(Separators, StringSplitOptions.RemoveEmptyEntries));
			string actionType = action.Dequeue();
			
			switch (actionType)
			{
				case "move":
					Move(action);
					break;
				case "take":
					Take(action);
					break;
				default:
					throw new Exception("Unrecognized action type: " + actionType);
			}

			actionIndex++;
		}

		private void Move(Queue<string> parameters)
		{
			//Vector source = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
			//Vector destination = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
			//owner.Move(source, destination);
		}

		private void Take(Queue<string> parameters)
		{
			//Vector source = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
			//Vector target = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
			//owner.Take(source, target);
		}
	}
}
