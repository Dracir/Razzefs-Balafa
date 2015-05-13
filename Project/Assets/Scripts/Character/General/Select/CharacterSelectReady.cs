using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterSelectReady : State, IInputListener {
	
	CharacterSelect Layer {
		get { return (CharacterSelect)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.selectMenu.skipUpdate = true;
		Layer.background.color = Layer.readyColor;
		Layer.wizardAnimator.enabled = false;
		Layer.inputSystem.GetKeyboardInfo("Controller").AddListener(this);
		Layer.inputSystem.GetJoystickInfo("Controller").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.inputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
		Layer.inputSystem.GetJoystickInfo("Controller").RemoveListener(this);
	}

	public void OnButtonInput(ButtonInput input) {
		switch (input.InputName) {
			case "Cycle":
				if (input.State == ButtonStates.Down) {
					SwitchState<CharacterSelectSelecting>();
				}
				
				break;
			case "Jump":
				if (input.State == ButtonStates.Down) {
					Layer.selectMenu.TryStartGame();
				}
				
				break;
			case "Menu":
				if (input.State == ButtonStates.Down) {
					Layer.selectMenu.TryStartGame();
				}
				
				break;
		}
	}

	public void OnAxisInput(AxisInput input) {
	}
}
