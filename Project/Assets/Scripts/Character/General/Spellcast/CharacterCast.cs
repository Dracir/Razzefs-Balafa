using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterCast : StateLayer, IInputListener {
	
	public Transform cursor;
	public float sensibility = 0.1F;
	public float smooth = 10;
	
	[Disable] public Vector2 currentAxis;
	[Disable] public Vector2 targetPosition;
	[Disable] public int currentSpell;
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
	
	bool _characterDetailCached;
	CharacterDetail _characterDetail;
	public CharacterDetail characterDetail { 
		get { 
			_characterDetail = _characterDetailCached ? _characterDetail : GetComponent<CharacterDetail>();
			_characterDetailCached = true;
			return _characterDetail;
		}
	}
	
	bool _cursorRendererCached;
	SpriteRenderer _cursorRenderer;
	public SpriteRenderer cursorRenderer { 
		get { 
			_cursorRenderer = _cursorRendererCached ? _cursorRenderer : cursor.GetComponent<SpriteRenderer>();
			_cursorRendererCached = true;
			return _cursorRenderer;
		}
	}
	
	System.Type[] spellTypes = {
		typeof(SpellGravityCast),
		typeof(SpellBlockCast), 
		typeof(SpellRainCast)
	};
	
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		inputSystem.GetKeyInfo("Cycle").AddListener(this);
		inputSystem.GetAxisInfo("AltMotionX").AddListener(this);
		inputSystem.GetAxisInfo("AltMotionY").AddListener(this);
		
		targetPosition = cursor.localPosition;
		currentSpell = System.Array.IndexOf(spellTypes, GetActiveState().GetType());
		
		cursor.gameObject.SetActive(true);
		cursorRenderer.color = characterDetail.color;
	}
	
	public override void OnExit() {
		base.OnExit();
		
		inputSystem.GetKeyInfo("Cycle").RemoveListener(this);
		inputSystem.GetAxisInfo("AltMotionX").RemoveListener(this);
		inputSystem.GetAxisInfo("AltMotionY").RemoveListener(this);
		
		cursor.gameObject.SetActive(false);
	}

	public void OnKeyInput(KeyInfo keyInfo, KeyStates keyState) {
		if (keyState == KeyStates.Down) {
			NextSpell();
		}
	}
	
	public void OnAxisInput(AxisInfo axisInfo, float axisValue) {
		switch (axisInfo.Name) {
			case "AltMotionX":
				currentAxis.x = axisValue;
				break;
			case "AltMotionY":
				currentAxis.y = axisValue;
				break;
		}
	}

	public void NextSpell() {
		currentSpell = (currentSpell + 1).Wrap(spellTypes.Length);
		
		SwitchState(spellTypes[currentSpell]);
	}
	
	public void UpdateCursor() {
		targetPosition += currentAxis * sensibility;
		targetPosition = Camera.main.ClampToScreen((Vector3)targetPosition + transform.position) - transform.position;
		cursor.TranslateLocalTowards(targetPosition, smooth, Axis.XY);
	}
}
