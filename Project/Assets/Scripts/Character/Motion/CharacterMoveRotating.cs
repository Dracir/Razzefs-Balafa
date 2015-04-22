using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterMoveRotating : State {
	
	[Range(0, 360)] public float offset;
	[Min] public float speed = 20;
	[Disable] public float currentAngle;
	[Disable] public float targetAngle;
	[Disable] public float currentFacingAngle;
	[Disable] public float targetFacingAngle;
	
	bool _modelTransformCached;
	Transform _modelTransform;
	public Transform modelTransform { 
		get { 
			_modelTransform = _modelTransformCached ? _modelTransform : transform.FindChild("Model").GetComponent<Transform>();
			_modelTransformCached = true;
			return _modelTransform;
		}
	}
	
	
	CharacterMove Layer {
		get { return ((CharacterMove)layer); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		targetAngle = -Layer.Gravity.Angle + 90;
		targetFacingAngle = Layer.HorizontalAxis > 0 ? offset : Layer.HorizontalAxis < 0 ? 180 + offset : targetFacingAngle;
		
		if (Mathf.Abs(currentFacingAngle - targetFacingAngle) > 0.0001F) {
			currentFacingAngle = Mathf.LerpAngle(modelTransform.localEulerAngles.y, targetFacingAngle, speed * Time.deltaTime);
			modelTransform.RotateLocalTowards(currentFacingAngle, speed, Axis.Y);
		}
	}
	
	public override void OnFixedUpdate() {
		if (Mathf.Abs(currentAngle - targetAngle) > 0.0001F) {
			currentAngle = Mathf.LerpAngle(transform.localEulerAngles.z, targetAngle, speed * Time.fixedDeltaTime);
//			Layer.rigidbody.RotateTowards(currentAngle, speed, Axis.Z);
		}
	}
}
