using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterMoveRotating : State {
	
	public bool invert;
	[Min] public float speed = 20;
	[Disable] public float currentAngle;
	[Disable] public float targetAngle;
	[Disable] public float currentFacingDirection;
	[Disable] public float targetFacingDirection;
	
	bool _spriteTransformCached;
	Transform _spriteTransform;
	public Transform spriteTransform { 
		get { 
			_spriteTransform = _spriteTransformCached ? _spriteTransform : transform.FindChild("Sprite").GetComponent<Transform>();
			_spriteTransformCached = true;
			return _spriteTransform;
		}
	}
	
	CharacterMove Layer {
		get { return ((CharacterMove)layer); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		targetAngle = -Layer.Gravity.Angle + 90;
		
		if (Layer.HorizontalAxis > 0) {
			targetFacingDirection = invert ? -1 : 1;
		}
		else if (Layer.HorizontalAxis < 0) {
			targetFacingDirection = invert ? 1 : -1;
		}
		
		if (Mathf.Abs(currentFacingDirection - targetFacingDirection) > 0.0001F) {
			currentFacingDirection = Mathf.Lerp(spriteTransform.localScale.x, targetFacingDirection, speed * Time.deltaTime);
			spriteTransform.ScaleLocalTowards(currentFacingDirection, speed, Axis.X);
		}
	}
	
	public override void OnFixedUpdate() {
		if (Mathf.Abs(currentAngle - targetAngle) > 0.0001F) {
			currentAngle = Mathf.LerpAngle(transform.localEulerAngles.z, targetAngle, speed * Time.fixedDeltaTime);
			Layer.rigidbody.RotateTowards(currentAngle, speed);
		}
	}
}
