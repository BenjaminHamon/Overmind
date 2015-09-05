using Overmind.Games.Engine;
using System.Collections.Generic;
using System.Linq;

namespace Overmind.Games.CityBuilder
{
	public class Main : IGameBuilder
	{
		public Game Create()
		{
			Game game = new Game(16);

			Player player = new Player(game, "Player");

			player.PieceCollection = new List<Piece>();

			player.ResourceCollection = new Dictionary<string, int>()
			{
				{ Player.GoldResource, 100 },
			};

			player.CommandCollection = new List<ICommand>() {
				new BuildCommand(player),
				new DestroyCommand(player),
			};

			game.AddPlayer(player);

			return game;
		}
	}
}
