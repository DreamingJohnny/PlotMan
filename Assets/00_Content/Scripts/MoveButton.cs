using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButton : MonoBehaviour {

	//They recieve a new move from the MoveSetter when they are clicked, and the UIHandler gets told, and tells them to turn of for a certain amount of time,
	//It probably does a IEnumerator for that....

	//And then, when pushed, it needs to be able to turn itself off,
	//And then, when enough time has passed, it needs to turn itself on again, right?

	public event EventHandler<OnMoveButtonPressedEventArgs> OnMoveButtonPressed;
	public class OnMoveButtonPressedEventArgs : EventArgs {
		public Move_Enum Move_Enum;
	}

	private Vector2 move;

	public Vector2 Move {
		get {
			return move;
		}
	}

	public Move_Enum Move_Enum { get; set; }

	public void SendMoveAction() {
		OnMoveButtonPressed?.Invoke(this, new OnMoveButtonPressedEventArgs { Move_Enum = Move_Enum });
	}

	public void SetNewMoveAction(SO_MoveAction sO_MoveAction) {

		GetComponent<Image>().sprite = sO_MoveAction.ButtonSprite;

		move = sO_MoveAction.Move;
		Move_Enum = sO_MoveAction.Move_Direction;
	}
}