using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterSelectSelecting : State, IInputListener {
	
	CharacterSelect Layer {
		get { return (CharacterSelect)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
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
			case "CastAction":
				if (input.State == ButtonStates.Down) {
					Wizardz previousWizard = Layer.wizardSelect.GetPreviousAvailableWizard(Layer.Wizard);
					InputManager.SwitchController(Layer.Wizard, previousWizard);
					Layer.Wizard = previousWizard;
				}
				
				break;
			case "Cycle":
				if (input.State == ButtonStates.Down) {
					InputManager.ReleaseController(Layer.Wizard);
					SwitchState<CharacterSelectIdle>();
				}
				
				break;
			case "Menu":
				if (input.State == ButtonStates.Down) {
					SwitchState<CharacterSelectReady>();
				}
				
				break;
		}
	}

	public void OnAxisInput(AxisInput input) {
	}
}
