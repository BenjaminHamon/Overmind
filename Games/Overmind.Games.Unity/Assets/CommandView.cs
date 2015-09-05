using Overmind.Games.Engine;
using Overmind.Unity;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class CommandView : MonoBehaviourBase
	{
		public void Initialize(PlayerView owner, ICommand command)
		{
			this.owner = owner;
			this.command = command;

			name = "Command " + command.Name;
			Label.text = command.Name;
		}

		public Text Label;

		private PlayerView owner;
		private ICommand command;

		public void Execute()
		{
			owner.CurrentCommand = command;
		}
	}
}
