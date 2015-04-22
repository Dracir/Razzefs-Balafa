using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterJump : StateLayer {
	
	public GroundCastSettings2D raySettings;
	[Disable] public Collider2D ground;
	
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
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		raySettings.angle = Gravity.Angle - 90;
		ground = raySettings.GetGround(transform.position, Vector3.down, Machine.Debug);
		
		if (ground == null) {
			Grounded = false;
		}
		else {
			Grounded = true;
			Layer.Friction = ground.sharedMaterial == null ? 1 : ground.sharedMaterial.friction;
		}
	}
}
