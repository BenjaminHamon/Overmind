using Overmind.Games.Engine;
using Overmind.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class PlayerView : MonoBehaviourBase
	{
		public void Initialize(Player player)
		{
			if (player == null)
				throw new ArgumentNullException("player");

			this.Player = player;
		}

		public Player Player { get; private set; }

		public override void Start()
		{
			name = "Player " + Player.Name;
			NameText.text = "Player " + Player.Name;

			Update();

			if (Player.CommandCollection != null)
			{
				foreach (ICommand command in Player.CommandCollection)
				{
					GameObject commandView = Instantiate<GameObject>(CommandButtonPrefab);
					commandView.GetComponent<CommandView>().Initialize(this, command);
					commandView.transform.SetParent(CommandCollectionTransform);
				}
			}

			if (Player.CanEndTurn)
				EndTurnButton.SetActive(true);
		}

		public override void Update()
		{
			ScoreText.text = Player.Score.ToString();
			if (Player.ResourceCollection != null)
			{
				KeyValuePair<string, int> resource = Player.ResourceCollection.First();
				ResourceText.text = resource.Key + " " + resource.Value;
			}
			else
				ResourceText.text = null;
		}

		[SerializeField]
		private Text NameText;
		[SerializeField]
		private Text ScoreText;
		[SerializeField]
		private Text ResourceText;

		[SerializeField]
		private Transform CommandCollectionTransform;
		[SerializeField]
		private GameObject CommandButtonPrefab;

		public ICommand CurrentCommand;

		public void OnCellClick(CellView sender)
		{
			if (CurrentCommand == null)
				Selection.Item = sender;
			else
			{
				CurrentCommand.Execute(sender.Position);
				CurrentCommand = null;
				Selection.Item = null;
			}
		}

		[SerializeField]
		private GameObject EndTurnButton;

		public void EndTurn()
		{
			Player.EndTurn();
		}

		public readonly Selection<CellView> Selection = new Selection<CellView>();
	}
}
