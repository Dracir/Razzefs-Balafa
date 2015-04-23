using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterJump : StateLayer {
	
	public Force2 Gravity {
		get {
			return Layer.gravity;
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
	
	public int JumpingHash {
		get {
			return Layer.jumpingHash;
		}
	}
	
	public Animator Animator { 
		get { 
			return Layer.animator;
		}
	}
	
	public Rigidbody2D Rigidbody { 
		get { 
			return Layer.rigidbody;
		}
	}
	
	public InputSystem InputSystem {
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
}
