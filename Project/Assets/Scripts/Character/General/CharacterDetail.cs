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
		if (jTenTrainDeDebugger) {
			IInputListener[] listeners = inputSystem.GetKeyboardInfo("Controller").GetListeners();
			inputSystem.RemoveKeyboardInfo("Controller");
			
			KeyboardButton[] keyboardButtons = {
				new KeyboardButton("Jump", KeyCode.Space),
				new KeyboardButton("Cycle", KeyCode.E),
				new KeyboardButton("CastAction", KeyCode.Mouse1),
				new KeyboardButton("Menu", KeyCode.Return),
				new KeyboardButton("Cast", KeyCode.Mouse0)
			};
			
			KeyboardAxis[] keyboardAxes = {
				new KeyboardAxis("MotionX", "Horizontal"),
				new KeyboardAxis("MotionY", "Vertical"),
				new KeyboardAxis("AltMotionX", "Mouse X"),
				new KeyboardAxis("AltMotionY", "Mouse Y")
			};
			
			KeyboardInfo keyboard = new KeyboardInfo("Controller", keyboardButtons, keyboardAxes, listeners);
			inputSystem.AddKeyboardInfo(keyboard);
			
			JoystickButton[] joystickButtons = {
				new JoystickButton("Jump", Joysticks.Any, JoystickButtons.Cross_A),
				new JoystickButton("Cycle", Joysticks.Any, JoystickButtons.Circle_B),
				new JoystickButton("CastAction", Joysticks.Any, JoystickButtons.Square_X),
				new JoystickButton("Menu", Joysticks.Any, JoystickButtons.Start),
				new JoystickButton("Cast", Joysticks.Any, JoystickButtons.R1),
				new JoystickButton("Cycle", Joysticks.Any, JoystickButtons.L1)
			};
			
			JoystickAxis[] joystickAxes = {
				new JoystickAxis("MotionX", Joysticks.Any, JoystickAxes.LeftStickX),
				new JoystickAxis("MotionY", Joysticks.Any, JoystickAxes.LeftStickY),
				new JoystickAxis("AltMotionX", Joysticks.Any, JoystickAxes.RightStickX),
				new JoystickAxis("AltMotionY", Joysticks.Any, JoystickAxes.RightStickY)
			};
			
			JoystickInfo joystick = new JoystickInfo("Controller", Joysticks.Any, joystickButtons, joystickAxes, listeners);
			inputSystem.AddJoystickInfo(joystick);
		}
	}
	
	void Start() {
		if (!jTenTrainDeDebugger) {
			InputManager.SetController(wizard, inputSystem);
		}
		
		temp = GetComponent<TemperatureInfo>();
	}
}

