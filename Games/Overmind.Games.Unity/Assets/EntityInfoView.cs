using Overmind.Games.Engine;
using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class EntityInfoView : MonoBehaviourBase
	{
		public EntityView Entity { get; private set; }

		public void SetEntity(EntityView entity, PlayerView activePlayer)
		{
			this.Entity = entity;
			UpdateCommands(activePlayer);
		}

		public override void Update()
		{
			if (Entity != null)
			{
				NameText.text = Entity.name;
			}
			else
			{
				NameText.text = null;
			}
		}

		[SerializeField]
		private Text NameText;

		[SerializeField]
		private Transform CommandGroup;
		[SerializeField]
		private GameObject CommandButtonPrefab;

		private void UpdateCommands(PlayerView activePlayer)
		{
			foreach (Transform commandView in CommandGroup)
				Destroy(commandView.gameObject);

			if ((Entity != null) && (Entity.Owner == activePlayer) && (Entity.Entity.CommandCollection != null))
			{
				foreach (ICommand command in Entity.Entity.CommandCollection)
				{
					GameObject commandView = Instantiate<GameObject>(CommandButtonPrefab);
					commandView.GetComponent<CommandView>().Initialize(Entity.Owner, command);
					commandView.transform.SetParent(CommandGroup);
				}
			}
		}
	}
}
