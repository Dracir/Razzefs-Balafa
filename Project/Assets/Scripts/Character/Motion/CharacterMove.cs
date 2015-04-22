using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterMove : StateLayer, IInputAxisListener {
	
	[Min] public float moveThreshold;
	[Min] public float inputPower = 1;
	[Disable] public float velocity;
	
	public Force2 Gravity {
		get {
			return Layer.Gravity;
		}
	}
	
	public float HorizontalAxis {
		get {
			return Layer.HorizontalAxis;
		}
		set {
			Layer.HorizontalAxis = value;
		}
	}
	
	public float AbsHorizontalAxis {
		get {
			return Layer.AbsHorizontalAxis;
		}
	}
	
	public float MoveVelocity {
		get {
			return Layer.MoveVelocity;
		}
		set {
			Layer.MoveVelocity = value;
		}
	}
	
	public float AbsMoveVelocity {
		get {
			return Layer.AbsMoveVelocity;
		}
	}
	
	public float Friction {
		get {
			return Layer.Friction;
		}
	}
	
	public Animator animator { 
		get { 
			return Layer.animator;
		}
	}
	
	new public Rigidbody2D rigidbody { 
		get { 
			return Layer.rigidbody;
		}
	}

	public InputSystem inputSystem {
		get {
			return Layer.inputSystem;
		}
	}
	
	CharacterMotion Layer {
		get { return ((CharacterMotion)layer); }
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}

	public override void OnEnter() {
		base.OnEnter();
		
		inputSystem.GetAxisInfo("MotionX").AddListener(this);
	}
	
	public override void OnExit() {
		base.OnExit();
		
		inputSystem.GetAxisInfo("MotionX").RemoveListener(this);
	}
	
	public void OnAxisInput(AxisInfo axisInfo, float axisValue) {
		HorizontalAxis = axisValue.PowSign(inputPower);
	}
}
