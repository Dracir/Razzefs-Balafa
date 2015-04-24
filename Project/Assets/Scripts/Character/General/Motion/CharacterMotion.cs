using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

[RequireComponent(typeof(InputSystem), typeof(Animator), typeof(Rigidbody2D))]
public class CharacterMotion : StateLayer {
	
	public Force2 gravity;
	public GroundCastSettings2D raySettings;
	
	[Disable] public Collider2D ground;
	
	[SerializeField, Disable] float horizontalAxis;
	public float HorizontalAxis {
		get {
			return horizontalAxis;
		}
		set {
			horizontalAxis = value;
			
			animator.SetFloat(absHorizontalAxisHash, AbsHorizontalAxis);
		}
	}
	
	public float AbsHorizontalAxis {
		get {
			return Mathf.Abs(horizontalAxis);
		}
	}
	
	[SerializeField, Disable] float moveVelocity;
	public float MoveVelocity {
		get {
			return moveVelocity;
		}
		set {
			moveVelocity = value;
			
			animator.SetFloat(absMoveVelocityHash, AbsMoveVelocity);
		}
	}
	
	public float AbsMoveVelocity {
		get {
			return Mathf.Abs(moveVelocity);
		}
	}
	
	[SerializeField, Disable] bool grounded;
	public bool Grounded {
		get {
			return grounded;
		}
		set {
			if (grounded != value) {
				grounded = value;
				animator.SetBool(groundedHash, grounded);
			}
		}
	}

	[SerializeField, Disable] bool jumping;
	public bool Jumping {
		get {
			return jumping;
		}
		set {
			if (jumping != value) {
				jumping = value;
				animator.SetBool(jumpingHash, jumping);
			}
		}
	}
	
	[SerializeField, Disable]
	float friction = 1;
	public float  Friction {
		get {
			return friction;
		}
		set {
			friction = value;
		}
	}
	
	[Disable] public int groundedHash = Animator.StringToHash("Grounded");
	[Disable] public int jumpingHash = Animator.StringToHash("Jumping");
	[Disable] public int absMoveVelocityHash = Animator.StringToHash("AbsMoveVelocity");
	[Disable] public int absHorizontalAxisHash = Animator.StringToHash("AbsHorizontalAxis");
	
	bool _animatorCached;
	Animator _animator;
	public Animator animator { 
		get { 
			_animator = _animatorCached ? _animator : GetComponent<Animator>();
			_animatorCached = true;
			return _animator;
		}
	}
	
	bool _rigidbodyCached;
	Rigidbody2D _rigidbody;
	new public Rigidbody2D rigidbody { 
		get { 
			_rigidbody = _rigidbodyCached ? _rigidbody : GetComponent<Rigidbody2D>();
			_rigidbodyCached = true;
			return _rigidbody;
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
	
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();

		raySettings.angle = gravity.Angle - 90;
		ground = raySettings.GetGround(transform.position, Vector3.down, Machine.Debug);
		
		if (ground == null) {
			Grounded = false;
		}
		else {
			Grounded = true;
			Friction = ground.sharedMaterial == null ? 1 : ground.sharedMaterial.friction;
		}
	}
	
	public override void OnFixedUpdate() {
		base.OnFixedUpdate();

		rigidbody.AddForce(gravity);
	}

	public void Enable() {
		foreach (IState states in GetActiveStates()) {
			states.SwitchState("Idle");
		}
	}
	
	public void Disable() {
		foreach (IState states in GetActiveStates()) {
			states.SwitchState<EmptyState>();
		}
	}
}
