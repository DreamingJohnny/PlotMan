using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MoveButton;

public class MoveSetSetter : MonoBehaviour {

	[SerializeField] private List<SO_MoveAction> sO_MoveActions;

	/// <summary>
	/// Receives an event, presumes sender is a moveButton and tries to send it a random SO_MoveAction.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public void HandleOnMoveButtonPressed(object sender, OnMoveButtonPressedEventArgs e) {

		MoveButton moveButton = (MoveButton)sender;
		moveButton.SetNewMoveAction(GetRandomSO_MoveAction());
	}

	public SO_MoveAction GetRandomSO_MoveAction() {
		if (sO_MoveActions.Count <= 0) {
			Debug.Log($"{sO_MoveActions} is empty and cannot give a new MoveAction.");
			return null;
		}

		int i = Random.Range(0, sO_MoveActions.Count);
		SO_MoveAction sO_MoveAction = Instantiate(sO_MoveActions[i]);
		return sO_MoveAction;
	}
}
