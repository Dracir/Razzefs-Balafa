using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCastIdle : State, IInputListener {
	
	SpellRainCast Layer {
		get { return (SpellRainCast)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.InputSystem.GetKeyboardInfo("Controller").AddListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").RemoveListener(this);
	}
	
	public void OnButtonInput(ButtonInput input) {
		switch (input.InputName) {
			case "Cast":
				if (input.State == ButtonStates.Down) {
					SwitchState<SpellRainCastCasting>();
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		
	}
}
