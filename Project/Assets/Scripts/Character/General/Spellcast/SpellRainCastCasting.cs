using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCastCasting : State, IInputKeyListener {
	
	public int maxSize = 6;
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
		
		Layer.InputSystem.GetKeyInfo("Cast").AddListener(this);
		startPosition = Layer.Cursor.position.Round();
		
		castZone = (Instantiate(Layer.castZone, startPosition, Quaternion.identity) as GameObject).transform;
		castZoneSprite = castZone.FindChild("Sprite");
		castZoneSpriteRenderer = castZoneSprite.GetComponent<SpriteRenderer>();
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
			SwitchState<SpellRainCastCooldown>();
		}
	}

	void UpdateCastZone() {
		currentSize = Mathf.Clamp(endPosition.x - startPosition.x, -maxSize, maxSize);
		castZoneSprite.SetLocalScale(currentSize, Axis.X);
		
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
		
		Vector2 position = new Vector2(startPosition.x + currentSize / 2, startPosition.y);
		
		activeRain = (Instantiate(Layer.rain, position, Quaternion.identity) as GameObject).GetComponent<FreezingRain>();
		activeRain.Width = Mathf.Abs(currentSize);
	}
}
