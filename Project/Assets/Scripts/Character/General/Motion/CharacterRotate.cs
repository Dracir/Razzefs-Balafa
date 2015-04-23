using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterRotate : StateLayer {
	
	public bool invert;
	
	[Disable] public float currentAngle;
	[Disable] public float targetAngle;
	[Disable] public float currentFacingDirection;
	[Disable] public float targetFacingDirection;
	
	public Rigidbody2D Rigidbody {
		get {
			return Layer.rigidbody;
		}
	}
	
	bool _spriteTransformCached;
	Transform _spriteTransform;
	public Transform spriteTransform { 
		get { 
			_spriteTransform = _spriteTransformCached ? _spriteTransform : transform.FindChild("Sprite").GetComponent<Transform>();
			_spriteTransformCached = true;
			return _spriteTransform;
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
		
		targetAngle = -Layer.gravity.Angle + 90;
		
		if (Layer.HorizontalAxis > 0) {
			targetFacingDirection = invert ? -1 : 1;
		}
		else if (Layer.HorizontalAxis < 0) {
			targetFacingDirection = invert ? 1 : -1;
		}
	}
}
