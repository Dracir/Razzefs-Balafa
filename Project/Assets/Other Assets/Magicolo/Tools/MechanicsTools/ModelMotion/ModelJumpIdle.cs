using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJumpIdle : State, IInputListener {
	
	ModelJump Layer {
		get { return ((ModelJump)layer); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.inputSystem.GetKeyboardInfo("Controller").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		Layer.inputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
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
