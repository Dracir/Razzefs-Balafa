using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDetail : MonoBehaviourExtended, IIdentifiable {
	
	public Wizardz wizard;
	
	[SerializeField, PropertyField] int id;
	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
	
	public Color color = Color.white;
	
	public bool jTenTrainDeDebugger = true;
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	TemperatureInfo temp;
	
	public float Temperature {
		get {
			return temp.Temperature;
		}
	}
	
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
	
	void Awake() {
		if (jTenTrainDeDebugger){
			IInputListener[] listeners = inputSystem.GetKeyboardInfo("Controller").GetListeners();
			inputSystem.RemoveKeyboardInfo("Controller");
			
			KeyboardButton[] buttons = {
				new KeyboardButton("Jump", KeyCode.Space),
				new KeyboardButton("Cycle", KeyCode.E),
				new KeyboardButton("CastAction", KeyCode.Mouse1),
				new KeyboardButton("Menu", KeyCode.Return),
				new KeyboardButton("Cast", KeyCode.Mouse0)
			};
			
			KeyboardAxis[] axes = {
				new KeyboardAxis("MotionX", "Horizontal"),
				new KeyboardAxis("MotionY", "Vertical"),
				new KeyboardAxis("AltMotionX", "Mouse X"),
				new KeyboardAxis("AltMotionY", "Mouse Y")
			};
			
			KeyboardInfo keyboard = new KeyboardInfo("Controller", buttons, axes, listeners);
			inputSystem.AddKeyboardInfo(keyboard);
		}
	}
	
	void Start() {
		if (!jTenTrainDeDebugger) {
			InputManager.SetController(wizard, inputSystem);
		}
		
		temp = GetComponent<TemperatureInfo>();
	}
}

