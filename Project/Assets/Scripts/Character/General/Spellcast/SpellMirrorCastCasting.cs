using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellMirrorCastCasting : State, IInputListener {
	
	[Min] public float distance = 2;
	[Min] public float fadeSpeed = 5;
	public SpellMirrorChargeLevel level1 = new SpellMirrorChargeLevel(15f, 150f, 1f, 0.1f, 0, 0, -1, new Color(0.48f, 0.67f, 0.75f, 1f));
	public SpellMirrorChargeLevel level2 = new SpellMirrorChargeLevel(25f, 150f, 1.5f, 0.15f, 2, 7, 5, new Color(0.43f, 0.43f, 0.51f, 1f));
	public SpellMirrorChargeLevel level3 = new SpellMirrorChargeLevel(35f, 150f, 1.5f, 0.2f, 4, 10, 3, new Color(0.28f, 0.28f, 0.28f, 1f));
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
		
		Layer.InputSystem.GetKeyboardInfo("Controller").AddListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").AddListener(this);
		
		startPosition = Layer.Cursor.position.Round();
		currentDirection = (startPosition - (Vector2)transform.position).normalized;
		currentPosition = (Vector2)transform.position + currentDirection * distance;
		currentChargeLevel = level1;
		
		castZone = (Instantiate(Layer.castZone, currentPosition, Quaternion.identity) as GameObject).transform;
		castZoneSprite = castZone.FindChild("Sprite");
		castZoneSpriteRenderer = castZoneSprite.GetComponent<SpriteRenderer>();
		
		if (activeBall != null) {
			activeBall.Explode();
			activeBall = null;
		}
	}

	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").RemoveListener(this);
		
		castZone.gameObject.Remove();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		endPosition = Layer.Cursor.position.Round();
		
		UpdateCastZone();
	}

	public void OnButtonInput(ButtonInput input) {
		switch (input.InputName) {
			case "Cast":
				if (input.State == ButtonStates.Up) {
					Cast();
					SwitchState<SpellMirrorCastCooldown>();
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		
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
		activeBall = (Instantiate(Layer.mirror, currentPosition, Quaternion.identity) as GameObject).GetComponent<MirrorBall>();
		activeBall.MinBounce = currentChargeLevel.minBounce;
		activeBall.MaxBounce = currentChargeLevel.maxBounce;
		activeBall.VelocityInherit = currentChargeLevel.velocityInherit;
		activeBall.Hotness = currentChargeLevel.hotness;
		activeBall.Direction = currentDirection;
		activeBall.Speed = currentChargeLevel.speed;
		activeBall.Bounces = currentChargeLevel.bounces;
		activeBall.Color = currentChargeLevel.color;
		
		currentCharge = 0;
	}
}


public class SpellMirrorChargeLevel {
	
	public float minBounce;
	public float maxBounce;
	public float velocityInherit;
	public float hotness;
	public float threshold;
	public float speed;
	public int bounces;
	public Color color = Color.white;
	
	public SpellMirrorChargeLevel(float minBounce, float maxBounce, float velocityInherit, float hotness, float threshold, float speed, int bounces, Color color)
	{
		this.minBounce = minBounce;
		this.maxBounce = maxBounce;
		this.velocityInherit = velocityInherit;
		this.hotness = hotness;
		this.threshold = threshold;
		this.speed = speed;
		this.bounces = bounces;
		this.color = color;
		
	}
}
