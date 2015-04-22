using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelMove : StateLayer, IInputAxisListener {
	
	[Min] public float moveThreshold;
	[Min] public float inputPower = 1;
	
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
	
	public Animator animator { 
		get { 
			return Layer.animator;
		}
	}
	
	new public Rigidbody rigidbody { 
		get { 
			return Layer.rigidbody;
		}
	}

	ModelMotion Layer {
		get { return ((ModelMotion)layer); }
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}

	public void OnAxisInput(AxisInfo axisInfo, float axisValue) {
		HorizontalAxis = axisValue.PowSign(inputPower);
	}
}
