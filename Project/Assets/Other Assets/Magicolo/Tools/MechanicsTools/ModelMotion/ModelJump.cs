using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class ModelJump : StateLayer {
	
	public GroundCastSettings raySettings;
	
	public Force2 Gravity {
		get {
			return Layer.Gravity;
		}
	}
	
	public bool Grounded {
		get {
			return Layer.Grounded;
		}
		set {
			Layer.Grounded = value;
		}
	}

	public bool Jumping {
		get {
			return Layer.Jumping;
		}
		set {
			Layer.Jumping = value;
		}
	}
	
	public int jumpingHash {
		get {
			return Layer.jumpingHash;
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
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
	
	ModelMotion Layer {
		get { return ((ModelMotion)layer); }
	}
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		raySettings.angle = Gravity.Angle - 90;
		Grounded = raySettings.HasHit(transform.position, Vector3.down, Machine.Debug);
	}
}
