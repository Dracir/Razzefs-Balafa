using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterCast : StateLayer, IInputAxisListener {
	
	public Transform cursor;
	public float sensibility = 0.1F;
	public float smooth = 10;
	
	[Disable] public Vector2 currentAxis;
	[Disable] public Vector2 targetPosition;
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
	
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		
		inputSystem.GetAxisInfo("AltMotionX").AddListener(this);
		inputSystem.GetAxisInfo("AltMotionY").AddListener(this);
		
		targetPosition = cursor.localPosition;
	}
	
	public override void OnExit() {
		base.OnExit();
		
		inputSystem.GetAxisInfo("AltMotionX").RemoveListener(this);
		inputSystem.GetAxisInfo("AltMotionY").RemoveListener(this);
	}

	public void OnAxisInput(AxisInfo axisInfo, float axisValue) {
		switch (axisInfo.Name) {
			case "AltMotionX":
				currentAxis.x = axisValue;
				break;
			case "AltMotionY":
				currentAxis.y = axisValue;
				break;
		}
	}
	
	public void UpdateCursor() {
		targetPosition += currentAxis * sensibility;
		targetPosition = Camera.main.ClampToScreen((Vector3)targetPosition + transform.position) - transform.position;
		cursor.TranslateLocalTowards(targetPosition, smooth, Axis.XY);
	}
}
