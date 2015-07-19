using Overmind.Unity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Overmind.Core;

namespace Overmind.Games.Unity
{
	public class CellView : MonoBehaviourBase, IPointerClickHandler
	{
		public PieceView Piece { get { return GetComponentInChildren<PieceView>(); } }
		public Vector Position;

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
				Select();
			else if (eventData.button == PointerEventData.InputButton.Right)
				Target();
		}

		public Image Border;

		public delegate void SelectedEventHandler(CellView sender);
		public event SelectedEventHandler Selected;

		private bool CanSelect { get { return Piece != null; } }

		private new void Select()
		{
			if (CanSelect == false)
				return;

			IsSelected = true;
			if (Selected != null)
				Selected(this);
		}

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

		public delegate void TargetedEventHandler(CellView sender);
		public event TargetedEventHandler Targeted;

		private void Target()
		{
			if (Targeted != null)
				Targeted(this);
		}

		//public void OnPointerClick(PointerEventData eventData)
		//{
		//	if (eventData.button == PointerEventData.InputButton.Left)
		//		Debug.Log("Left click");
		//	else if (eventData.button == PointerEventData.InputButton.Middle)
		//		Debug.Log("Middle click");
		//	else if (eventData.button == PointerEventData.InputButton.Right)
		//		Debug.Log("Right click");
		//}

		//private void Select()
		//{

		//}
	}
}
