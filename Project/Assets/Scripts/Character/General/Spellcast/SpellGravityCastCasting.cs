using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellGravityCastCasting : State, IInputKeyListener {
	
	[Min] public int maxSize = 8;
	
	[Disable] public Vector2 startPosition;
	[Disable] public Vector2 endPosition;
	[Disable] public float currentAngle;
	[Disable] public float currentSize;
	[Disable] public Transform castZone;
	[Disable] public Transform castZoneSprite;
	[Disable] public GravityWell activeGravityWell;
	
	SpellGravityCast Layer {
		get { return (SpellGravityCast)layer; }
	}
	
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.InputSystem.GetKeyInfo("Cast").AddListener(this);
		startPosition = Layer.Cursor.position.Round();
		
		castZone = (Instantiate(Layer.castZone, startPosition, Quaternion.identity) as GameObject).transform;
		castZoneSprite = castZone.FindChild("Sprite");
	}

	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyInfo("Cast").RemoveListener(this);
		
		castZone.gameObject.Remove();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		endPosition = Layer.Cursor.position.Round();
		
		UpdateCastZone();
	}

	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		if (keyState == KeyStates.Up) {
			Cast();
			SwitchState<SpellGravityCastCooldown>();
		}
	}
	
	void UpdateCastZone() {
		Vector2 difference = endPosition - startPosition;
		
		currentSize = Mathf.Min(difference.magnitude, maxSize);
		currentAngle = -difference.Angle();
		
		castZone.SetLocalEulerAngles(currentAngle, Axis.Z);
		castZoneSprite.SetLocalScale(currentSize, Axis.X);
	}
	
	void Cast() {
		if (activeGravityWell != null) {
			activeGravityWell.gameObject.Remove();
		}
		
		if (currentSize < 1) {
			return;
		}
		
		activeGravityWell = (Instantiate(Layer.gravityWell, startPosition, Quaternion.Euler(0, 0, currentAngle)) as GameObject).GetComponent<GravityWell>();
		activeGravityWell.Angle = currentAngle;
		activeGravityWell.Length = currentSize;
	}
}