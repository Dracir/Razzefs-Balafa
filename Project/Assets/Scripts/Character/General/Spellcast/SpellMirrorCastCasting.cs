using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellMirrorCastCasting : State, IInputKeyListener {
	
	[Min] public float distance = 2;
	[Min] public float fadeSpeed = 5;
	public SpellMirrorChargeLevel level1;
	public SpellMirrorChargeLevel level2;
	public SpellMirrorChargeLevel level3;
	[Min] public float maxCharge = 5;
	
	[Disable] public Vector2 startPosition;
	[Disable] public Vector2 endPosition;
	[Disable] public Vector2 currentDirection;
	[Disable] public Vector2 currentPosition;
	[Disable] public float currentCharge;
	[Disable] public Transform castZone;
	[Disable] public Transform castZoneSprite;
	[Disable] public SpriteRenderer castZoneSpriteRenderer;
	[Disable] public MirrorBall activeBall;
	
	SpellMirrorChargeLevel currentChargeLevel;
	
	SpellMirrorCast Layer {
		get { return (SpellMirrorCast)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.InputSystem.GetKeyInfo("Cast").AddListener(this);
		
		startPosition = Layer.Cursor.position.Round();
		currentDirection = (startPosition - (Vector2)transform.position).normalized;
		currentPosition = (Vector2)transform.position + currentDirection * distance;
		currentChargeLevel = level1;
		
		castZone = (Instantiate(Layer.castZone, currentPosition, Quaternion.identity) as GameObject).transform;
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
			SwitchState<SpellMirrorCastCooldown>();
		}
	}

	void UpdateCastZone() {
		currentCharge += Time.deltaTime;
		UpdateChargeLevel();
		
		if (currentCharge > maxCharge) {
			Cast();
			activeBall.Explode();
			SwitchState<SpellMirrorCastCooldown>();
			return;
		}
		
		currentDirection = (endPosition - (Vector2)transform.position).normalized;
		currentPosition = (Vector2)transform.position + currentDirection * distance;
		
		castZone.position = currentPosition;
	}
	
	void UpdateChargeLevel() {
		if (currentCharge >= level3.threshold) {
			currentChargeLevel = level3;
		}
		else if (currentCharge >= level2.threshold) {
			currentChargeLevel = level2;
		}
		else if (currentCharge >= level1.threshold) {
			currentChargeLevel = level1;
		}
		
		castZoneSpriteRenderer.FadeTowards(currentChargeLevel.color, fadeSpeed);
	}
	
	void Cast() {
		if (activeBall != null) {
			activeBall.gameObject.Remove();
		}
		
		activeBall = (Instantiate(Layer.mirror, currentPosition, Quaternion.identity) as GameObject).GetComponent<MirrorBall>();
		activeBall.Direction = currentDirection;
		activeBall.Speed = currentChargeLevel.speed;
		activeBall.Bounces = currentChargeLevel.bounces;
		activeBall.Color = currentChargeLevel.color;
		
		currentCharge = 0;
	}
}

[System.Serializable]
public class SpellMirrorChargeLevel {
	
	[Min] public float threshold;
	[Min] public float speed;
	[Min] public int bounces;
	public Color color = Color.white;
}