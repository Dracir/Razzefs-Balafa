using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCastCasting : State, IInputListener {
	
	[Min] public float baseHeatCost = 0.05F;
	[Min] public float heatCostPerSize = 0.05F;
	[Min] public int maxSize = 6;
	[Range(0, 0.5F)] public float margin = 0.25F;
	public LayerMask layerMask;
	public Color validColor = new Color(0, 1, 0, 0.125F);
	public Color invalidColor = new Color(1, 0, 0, 0.125F);
	
	[Disable] public Vector2 startPosition;
	[Disable] public Vector2 endPosition;
	[Disable] public float currentSize;
	[Disable] public bool valid;
	[Disable] public Transform castZone;
	[Disable] public Transform castZoneSprite;
	[Disable] public SpriteRenderer castZoneSpriteRenderer;
	[Disable] public FreezingRain activeRain;
	
	SpellRainCast Layer {
		get { return (SpellRainCast)layer; }
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
		castZoneSpriteRenderer = castZoneSprite.GetComponent<SpriteRenderer>();
		
		if (activeRain != null) {
			activeRain.Explode();
			activeRain = null;
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
					SwitchState<SpellRainCastCooldown>();
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		
	}
	
	void UpdateCastZone() {
		currentSize = Mathf.Clamp(endPosition.x - startPosition.x, -maxSize, maxSize);
		castZoneSprite.SetLocalScale(currentSize, Axes.X);
		
		Vector2 pointA = new Vector2(startPosition.x - 0.5F + margin, startPosition.y - 0.5F + margin);
		Vector2 pointB = new Vector2(startPosition.x + currentSize - 0.5F - margin, startPosition.y + 0.5F - margin);

		if (Physics2D.OverlapArea(pointA, pointB, layerMask) != null) {
			valid = false;
			castZoneSpriteRenderer.color = invalidColor;
		}
		else {
			valid = true;
			castZoneSpriteRenderer.color = validColor;
		}
	}
	
	void Cast() {
		if (activeRain != null) {
			activeRain.gameObject.Remove();
		}
		
		if (Mathf.Abs(currentSize) < 1 || !valid) {
			return;
		}
		
		Vector2 position = new Vector2(startPosition.x + currentSize / 2 - 0.5F, startPosition.y);
		
		activeRain = (Instantiate(Layer.rain, position, Quaternion.identity) as GameObject).GetComponent<FreezingRain>();
		activeRain.Width = Mathf.Abs(currentSize);
		
		Layer.TemperatureInfo.Heat(baseHeatCost + heatCostPerSize * Mathf.Abs(currentSize));
		Layer.AudioPlayer.Play("SpellCastRain");
	}
}
