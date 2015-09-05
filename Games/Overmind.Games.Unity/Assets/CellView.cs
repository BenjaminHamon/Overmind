using Overmind.Core;
using Overmind.Unity;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Overmind.Games.Unity
{
	public class CellView : MonoBehaviourBase, IPointerClickHandler, ISelectable
	{
		public EntityView Entity { get { return GetComponentInChildren<EntityView>(); } }
		public Vector Position;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
				if (Clicked != null)
					Clicked(this);
		}

		public event Action<CellView> Clicked;

		[SerializeField]
		private Image Border;

		private bool isSelected;
		public bool IsSelected
		{
			get { return isSelected; }
			set
			{
				if (value == isSelected)
					return;
				isSelected = value;
				Border.color = isSelected ? Color.green : Color.black;
			}
		}
	}
}
