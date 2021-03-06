using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellGravityCastCasting : State, IInputListener {
	
	[Min] public float baseHeatCost = 0.05F;
	[Min] public float heatCostPerSize = 0.04F;
	[Min] public int maxSize = 8;
	
	[Disable] public Vector2 startPosition;
	[Disable] public Vector2 endPosition;
	[Disable] public float currentAngle;
	[Disable] public float currentSize;
	[Disable] public Transform castZone;
	[Disable] public Transform castZoneSprite;
	[Disable] public Transform castZonePortal;
	[Disable] public GravityWell activeGravityWell;
	
	SpellGravityCast Layer {
		get { return (SpellGravityCast)layer; }
	}
	
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.InputSystem.GetKeyboardInfo("Controller").AddListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").AddListener(this);
		Layer.MaxCursorRange = maxSize;
		Layer.CursorOffset = Layer.LocalCursorTarget;
		Layer.LocalCursorTarget = Vector2.zero;
		
		startPosition = (Layer.WorldCursorTarget + Layer.CursorOffset).Round();
		
		castZone = (Instantiate(Layer.castZone, startPosition, Quaternion.identity) as GameObject).transform;
		castZoneSprite = castZone.FindChild("Sprite");
		castZonePortal = castZone.FindChild("Portal");
		
		if (activeGravityWell != null) {
			activeGravityWell.gameObject.Remove();
			activeGravityWell = null;
		}
	}

	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").RemoveListener(this);
		Layer.MaxCursorRange = Layer.maxCursorRange;
		Layer.LocalCursorTarget = Layer.CursorOffset;
		Layer.CursorOffset = Vector2.zero;
		
		castZone.gameObject.Remove();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		endPosition = (Layer.WorldCursorTarget + Layer.CursorOffset).Round();
		
		UpdateCastZone();
	}

	public void OnButtonInput(ButtonInput input) {
		switch (input.InputName) {
			case "Cast":
				if (input.State == ButtonStates.Up) {
					Cast();
					SwitchState<SpellGravityCastCooldown>();
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		
	}
	
	void UpdateCastZone() {
		Vector2 difference = endPosition - startPosition;
		
		currentSize = Mathf.Min(difference.magnitude, maxSize);
		currentAngle = -difference.Angle();
		
		castZone.SetLocalEulerAngles(currentAngle, Axes.Z);
		castZoneSprite.SetLocalScale(currentSize, Axes.X);
		castZonePortal.gameObject.SetActive(currentSize >= 1);
	}
	
	void Cast() {
		if (currentSize < 1) {
			return;
		}
		
		activeGravityWell = (Instantiate(Layer.gravityWell, startPosition, Quaternion.Euler(0, 0, currentAngle)) as GameObject).GetComponent<GravityWell>();
		activeGravityWell.Angle = currentAngle;
		activeGravityWell.Length = currentSize;
		
		Layer.TemperatureInfo.Heat(baseHeatCost + heatCostPerSize * currentSize);
		Layer.AudioPlayer.Play("SpellCastGravity");
	}
}
