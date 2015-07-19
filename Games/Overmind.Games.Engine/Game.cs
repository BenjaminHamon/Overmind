using System;
using System.Collections.Generic;
using System.Linq;

namespace Overmind.Games.Engine
{
	public class Game : IDisposable
	{
		public Game(int boardSize)
		{
			this.BoardSize = boardSize;
		}

		public readonly int BoardSize;

		public void Start()
		{
			Turn = 1;
			ActivePlayer = playerCollection.First();
		}

		public void AddPlayer(Player player)
		{
			playerCollection.Add(player);
		}

		//private void AddPlayer(string playerName, IEnumerable<Vector> pieceSetup, IRule rule)
		//{
		//	ConstructorInfo strategyConstructor = scriptAssembly.GetType("GoldenAge.Scripts.ScriptedStrategy")
		//		.GetConstructor(new Type[] { typeof(Player) });

		//	Player player = new Player(this, playerName, pieceSetup, rule);
		//	player.Strategy = (IStrategy)strategyConstructor.Invoke(new object[] { player });

		//	playerCollection.Add(player);
		//}

		private readonly List<Player> playerCollection = new List<Player>();
		public IEnumerable<Player> PlayerCollection { get { return playerCollection.AsReadOnly(); } }
		public Player ActivePlayer { get; private set; }

		public IEnumerable<Piece> AllPieces
		{
			get 
			{
				return playerCollection.Select(player => player.PieceCollection)
					.Cast<IEnumerable<Piece>>().Aggregate((first, second) => first.Concat(second));
			}
		}

		public int Turn { get; private set; }

		public void NextTurn()
		{
			ActivePlayer.Update();

			Turn++;
			ActivePlayer = playerCollection.ElementAtOrDefault(playerCollection.IndexOf(ActivePlayer) + 1) ?? playerCollection.First();
		}

		//public void Add(Unit unit)
		//{
		//	unitCollection.Add(unit);
		//}

		//public Unit GetActiveUnit()
		//{
		//	return unitCollection.First();
		//}

		//public IEnumerable<Unit> GetUnitsInRange(Vector center, double range)
		//{
		//	return unitCollection.Where(unit => (unit.Position - center).Norm() <= range);
		//}

		public void Dispose()
		{
			foreach (Player player in playerCollection)
				player.Dispose();
		}
	}
}
