using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class ModView : MonoBehaviourBase
	{
		private string modField;
		public string Mod
		{
			get { return modField; }
			set
			{
				modField = value;
				Title.text = modField;
			}
		}

		[SerializeField]
		private Text Title;

		public void StartMod()
		{
			Debug.Log("Starting " + Mod);

			UnityApplication.Instance.Mod = Mod;
			Application.LoadLevel("Game");
		}
	}
}
