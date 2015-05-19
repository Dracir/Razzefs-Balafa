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
		typeof(SpellRainCast), 
		typeof(SpellMirrorCast)
	};
	
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		inputSystem.GetKeyboardInfo("Controller").AddListener(this);
		inputSystem.GetJoystickInfo("Controller").AddListener(this);
		
		targetPosition = cursor.localPosition;
		currentSpell = System.Array.IndexOf(spellTypes, GetActiveState().GetType());
		
		cursor.gameObject.SetActive(true);
		cursorRenderer.color = characterDetail.color;
	}
	
	public override void OnExit() {
		base.OnExit();
		
		inputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
		inputSystem.GetJoystickInfo("Controller").RemoveListener(this);
		
		cursor.gameObject.SetActive(false);
	}

	public void OnButtonInput(ButtonInput input) {
		switch (input.InputName) {
			case "Cycle":
				if (input.State == ButtonStates.Down) {
					NextSpell();
				}
				break;
		}
	}
	
	public void OnAxisInput(AxisInput input) {
		switch (input.InputName) {
			case "AltMotionX":
				currentAxis.x = input.Value;
				break;
			case "AltMotionY":
				currentAxis.y = input.Value;
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
		cursor.TranslateLocalTowards(targetPosition, smooth, Axes.XY);
	}

	public void Enable() {
		inputSystem.GetKeyboardInfo("Controller").AddListener(this);
		inputSystem.GetJoystickInfo("Controller").AddListener(this);
		
		foreach (IState state in GetActiveStates()) {
			state.SwitchState("Idle");
		}
	}
	
	public void Disable() {
		inputSystem.GetKeyboardInfo("Controller").RemoveListener(this);
		inputSystem.GetJoystickInfo("Controller").RemoveListener(this);
		
		foreach (IState state in GetActiveStates()) {
			state.SwitchState<EmptyState>();
		}
	}
}
