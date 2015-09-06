//using Overmind.Games.Engine;
//using Overmind.Core;
//using System;
//using System.Collections.Generic;
//using System.IO;

//namespace Overmind.Games.Checkers
//{
//	public class ScriptedStrategy : IStrategy, IDisposable
//	{
//		private const string ScriptDirectory = @"E:\Projects\Overmind\Games\Mods\Checkers";

//		public ScriptedStrategy(Player owner)
//		{
//			this.owner = owner;
//			string filePath = Path.Combine(ScriptDirectory, String.Format("ScriptedStrategy_{0}.txt", owner.Name));
//			actionReader = new StreamReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
//		}

//		private readonly Player owner;
//		private readonly TextReader actionReader;

//		private char[] Separators = { ' ' };

//		public void Update()
//		{
//			Queue<string> action = new Queue<string>(actionReader.ReadLine().Split(Separators, StringSplitOptions.RemoveEmptyEntries));
//			string actionType = action.Dequeue();
			
//			switch (actionType)
//			{
//				case "move":
//					Move(action);
//					break;
//				case "take":
//					Take(action);
//					break;
//				default:
//					throw new Exception("Unrecognized action type: " + actionType);
//			}
//		}

//		private void Move(Queue<string> parameters)
//		{
//			Vector source = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
//			Vector destination = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
//			owner.Move(source, destination);
//		}

//		private void Take(Queue<string> parameters)
//		{
//			Vector source = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
//			Vector target = new Vector(Double.Parse(parameters.Dequeue()), Double.Parse(parameters.Dequeue()));
//			owner.Take(source, target);
//		}

//		public void Dispose()
//		{
//			actionReader.Dispose();
//		}
//	}
//}
