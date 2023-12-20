using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	//This reference should be moved to the gameHandler later on.
	[SerializeField] private MoveSetSetter moveSetSetter;

	[SerializeField] private List<Button> moveButtons;
	[SerializeField] private Button powerButton;
	[SerializeField] private Slider powerSlider;
	[SerializeField] private Slider healthSlider;

	public event EventHandler OnPowerButtonClicked;

	void Start() {

	}

	public void SetUpAllButtons(PlayerController playerController) {

		SetUpMoveButtons(playerController);
		SetUpPowerButton(playerController);
		
		if (playerController.gameObject.TryGetComponent(out Health health)) {
			SetUpHealthSlider(health);
		}
	}

	private void SetUpMoveButtons(PlayerController playerController) {

		foreach (Button button in moveButtons) {
			if (button.TryGetComponent(out MoveButton component)) {
				component.SetNewMoveAction(moveSetSetter.GetRandomSO_MoveAction());
				
				component.OnMoveButtonPressed += playerController.HandleOnMoveReceived;
				component.OnMoveButtonPressed += moveSetSetter.HandleOnMoveButtonPressed;
			}
		}
	}

	private void SetUpPowerButton(PlayerController playerController) {
		OnPowerButtonClicked += playerController.HandleOnPowerButtonClicked;
		playerController.OnPowerChanged += HandleOnPowerChanged;

		powerSlider.value = playerController.CurrentPower / playerController.MaxPower;
	}

	private void SetUpHealthSlider(Health health) {
		healthSlider.value = health.CurrentHealth / health.MaxHealth;

		health.OnHealthChanged += HandleOnHealthChanged;
	}

	public void PowerButtonClicked() {
		OnPowerButtonClicked?.Invoke(this, EventArgs.Empty);
	}

	public void HandleOnPowerChanged(object sender, EventArgs e) {
		PlayerController playerController = (PlayerController)sender;

		powerSlider.value = playerController.CurrentPower / playerController.MaxPower;
	}

	public void HandleOnHealthChanged(object sender, EventArgs e) {
		Health health = (Health)sender;

		healthSlider.value = health.CurrentHealth / health.MaxHealth;
	}
}