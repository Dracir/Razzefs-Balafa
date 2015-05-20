using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellBlockCastCasting : State, IInputListener {
	
	[Min] public float baseHeatCost = 0.05F;
	[Min] public float heatCostPerSize = 0.1F;
	[Min] public int maxSize = 3;
	[Min] public int maxArea = 10;
	[Range(0, 0.5F)] public float margin = 0.25F;
	public LayerMask layerMask;
	public Color validColor = new Color(0, 1, 0, 0.125F);
	public Color invalidColor = new Color(1, 0, 0, 0.125F);
	
	[Disable] public Vector2 startPosition;
	[Disable] public Vector2 endPosition;
	[Disable] public int currentSize = 1;
	[Disable] public int currentBlockArea;
	[Disable] public bool valid;
	[Disable] public Transform castZone;
	[Disable] public Transform castZoneSprite;
	[Disable] public SpriteRenderer castZoneSpriteRenderer;
	
	Queue<Block> activeBlocks = new Queue<Block>();
	
	SpellBlockCast Layer {
		get { return (SpellBlockCast)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.InputSystem.GetKeyboardInfo("Controller").AddListener(this);
		Layer.InputSystem.GetJoystickInfo("Controller").AddListener(this);
		startPosition = Layer.Cursor.position.Round();
		
		currentSize = 1;
		castZone = (Instantiate(Layer.castZone, startPosition, Quaternion.identity) as GameObject).transform;
		castZoneSprite = castZone.FindChild("Sprite");
		castZoneSprite.SetLocalScale(currentSize, Axes.XY);
		castZoneSpriteRenderer = castZoneSprite.GetComponent<SpriteRenderer>();
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
					SwitchState<SpellBlockCastCooldown>();
				}
				break;
			case "CastAction":
				if (input.State == ButtonStates.Down) {
					NextSize();
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		
	}

	void UpdateCastZone() {
		castZone.SetPosition(endPosition);
		
		Vector2 pointA = new Vector2(endPosition.x - 0.5F + margin, endPosition.y - 0.5F + margin);
		Vector2 pointB = new Vector2(endPosition.x + currentSize - 0.5F - margin, endPosition.y + currentSize - 0.5F - margin);

		if (Physics2D.OverlapArea(pointA, pointB, layerMask) != null) {
			valid = false;
			castZoneSpriteRenderer.color = invalidColor;
		}
		else {
			valid = true;
			castZoneSpriteRenderer.color = validColor;
		}
	}
	
	void NextSize() {
		currentSize = (currentSize + 1).Wrap(maxSize + 1);
		castZoneSprite.SetLocalScale(currentSize, Axes.XY);
	}
	
	void Cast() {
		if (currentSize < 1 || !valid) {
			return;
		}
		
		currentBlockArea = GetCurrentArea();
		currentBlockArea += currentSize.Pow(2);
		
		RemoveOverflow();
		
		Block block = (Instantiate(Layer.block, endPosition, Quaternion.identity) as GameObject).GetComponent<Block>();
		block.Size = currentSize;
		
		activeBlocks.Enqueue(block);
		
		Layer.TemperatureInfo.Temperature += baseHeatCost + heatCostPerSize * currentSize;
		Layer.AudioPlayer.Play("SpellCastBlock").ApplyOptions(AudioOption.Pitch(1.25F / currentSize), AudioOption.RandomPitch(0.5F));
	}

	int GetCurrentArea() {
		int area = 0;
		
		foreach (Block block in activeBlocks) {
			if (block != null) {
				area += block.Area;
			}
		}
		
		return area;
	}
	
	void RemoveOverflow() {
		while (currentBlockArea > maxArea) {
			Block block = activeBlocks.Dequeue();
			
			if (block != null) {
				currentBlockArea -= block.Area;
				block.Explode();
			}
		}
	}
}
