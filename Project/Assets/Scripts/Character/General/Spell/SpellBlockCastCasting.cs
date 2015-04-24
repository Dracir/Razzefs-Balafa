using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellBlockCastCasting : State, IInputKeyListener {
	
	public int maxSize = 3;
	public int maxArea = 10;
	public float margin = 0.25F;
	public LayerMask layerMask;
	public Color validColor = Color.green;
	public Color invalidColor = Color.red;
	
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
		
		Layer.InputSystem.GetKeyInfo("Cast").AddListener(this);
		Layer.InputSystem.GetKeyInfo("CastAction").AddListener(this);
		startPosition = Layer.Cursor.position.Round();
		
		castZone = (Instantiate(Layer.castZone, startPosition, Quaternion.identity) as GameObject).transform;
		castZoneSprite = castZone.FindChild("Sprite");
		castZoneSprite.SetLocalScale(currentSize, Axis.XY);
		castZoneSpriteRenderer = castZoneSprite.GetComponent<SpriteRenderer>();
	}

	public override void OnExit() {
		base.OnExit();
		
		Layer.InputSystem.GetKeyInfo("Cast").RemoveListener(this);
		Layer.InputSystem.GetKeyInfo("CastAction").RemoveListener(this);
		
		castZone.gameObject.Remove();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		endPosition = Layer.Cursor.position.Round();
		
		UpdateCastZone();
	}

	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		switch (keyInfo.Name) {
			case "Cast":
				if (keyState == KeyStates.Up) {
					Cast();
					SwitchState<SpellBlockCastCooldown>();
				}
				break;
			case "CastAction":
				if (keyState == KeyStates.Down) {
					NextSize();
				}
				break;
		}
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
		castZoneSprite.SetLocalScale(currentSize, Axis.XY);
	}
	
	void Cast() {
		if (currentSize < 1 || !valid) {
			return;
		}
		
		currentBlockArea += currentSize.Pow(2);
		
		RemoveOverflow();
		
		Block block = (Instantiate(Layer.block, endPosition, Quaternion.identity) as GameObject).GetComponent<Block>();
		block.Size = currentSize;
		
		activeBlocks.Enqueue(block);
	}

	void RemoveOverflow() {
		while (currentBlockArea > maxArea) {
			Block block = activeBlocks.Dequeue();
			
			currentBlockArea -= block.Area;
			block.gameObject.Remove();
		}
	}
}
