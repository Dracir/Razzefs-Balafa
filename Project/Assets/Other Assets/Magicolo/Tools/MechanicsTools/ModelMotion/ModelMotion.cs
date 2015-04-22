using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

[RequireComponent(typeof(InputSystem), typeof(Animator), typeof(Rigidbody))]
public class ModelMotion : StateLayer {
	
	[SerializeField, PropertyField]
	Force2 gravity;
	public Force2 Gravity {
		get {
			return gravity;
		}
		set {
			gravity = value;
		}
	}

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
	
	[Disable] public int groundedHash = Animator.StringToHash("Grounded");
	[Disable] public int jumpingHash = Animator.StringToHash("Jumping");
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
	Rigidbody _rigidbody;
	new public Rigidbody rigidbody { 
		get { 
			_rigidbody = _rigidbodyCached ? _rigidbody : GetComponent<Rigidbody>();
			_rigidbodyCached = true;
			return _rigidbody;
		}
	}

	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnFixedUpdate() {
		base.OnFixedUpdate();

		rigidbody.AddForce(Gravity);
	}
}
