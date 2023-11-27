using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	[SerializeField] private PlayerController playerController;
	//This reference should be moved to the gameHandler later on.
	[SerializeField] private MoveSetSetter moveSetSetter;

	[SerializeField] private List<Button> moveButtons;
	[SerializeField] private Button powerButton;
	[SerializeField] private Slider powerSlider;
	[SerializeField] private Slider healthSlider;

	public event EventHandler OnPowerButtonClicked;


	void Start() {

		SetUpMoveButtons();
	}

	private void SetUpMoveButtons() {

		foreach (Button button in moveButtons) {
			if (button.TryGetComponent(out MoveButton component)) {
				component.SetNewMoveAction(moveSetSetter.GetRandomSO_MoveAction());
				
				component.OnMoveButtonPressed += playerController.HandleOnMoveReceived;
				component.OnMoveButtonPressed += moveSetSetter.HandleOnMoveButtonPressed;
			}
		}

		OnPowerButtonClicked += playerController.HandleOnPowerButtonClicked;
		playerController.OnHealthChanged += HandleOnHealthChanged;
		playerController.OnPowerChanged += HandleOnPowerChanged;
	}

	public void PowerButtonClicked() {
		OnPowerButtonClicked?.Invoke(this, EventArgs.Empty);
	}

	public void HandleOnPowerChanged(object sender, EventArgs e) {

	}

	public void HandleOnHealthChanged(object sender, EventArgs e) {

		//So this should look at the sender, and get health, or whatever, from that then?
	}
}
