using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterJumpIdle : State, IInputListener {
	
	CharacterJump Layer {
		get { return ((CharacterJump)layer); }
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
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		if (!Layer.Grounded) {
			SwitchState("Falling");
			return;
		}
	}
	
	public void OnButtonInput(ButtonInput input) {
		switch (input.InputName) {
			case "Jump":
				if (input.State == ButtonStates.Down) {
			SwitchState("Jumping");
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		
	}
}
