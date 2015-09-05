using Overmind.Core;
using Overmind.Games.Engine;
using System;

namespace Overmind.Games.CityBuilder
{
	public class Player : Engine.Player
	{
		public const string GoldResource = "Gold";

		public Player(Game game, string name)
			: base(game, name)
		{ }

		public void Build(Vector position)
		{
			if (FindEntity<Piece>(position) != null)
				throw new Exception("[Player.Build] Position is already occupied.");
			if (ResourceCollection[GoldResource] < 10)
				throw new Exception("[Player.Build] Not enough gold.");

			ResourceCollection[GoldResource] -= 10;
			AddEntity(new House(this, position));
		}

		public void Destroy(Vector position)
		{
			Piece entity = FindEntity<Piece>(position);
			if (entity == null)
				throw new Exception("[Player.Destroy] No entity found.");

			entity.Destroy();
		}

		protected override void Update()
		{
			foreach (House house in PieceCollection)
			{
				ResourceCollection[GoldResource] += 5;
			}
		}
	}
}
