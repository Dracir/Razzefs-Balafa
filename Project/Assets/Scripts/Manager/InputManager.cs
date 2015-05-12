using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.GeneralTools;

public class InputManager : MonoBehaviourExtended {

	static InputManager instance;
	static InputManager Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<InputManager>();
			}
			
			return instance;
		}
	}
	
	[Min] public int maxPlayers = 4;
	
	Dictionary<Wizardz, ControllerInfo> wizardControllerDict;
	public Dictionary<Wizardz, ControllerInfo> WizardControllerDict {
		get {
			if (wizardControllerDict == null) {
				wizardControllerDict = new Dictionary<Wizardz, ControllerInfo>();
			}
			
			return wizardControllerDict;
		}
	}
	
	[Disable] public List<Joysticks> activeJoysticks = new List<Joysticks>();
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
    
	public static void SetController(Wizardz wizard, InputSystem system) {
		KeyboardInfo keyboard = Instance.WizardControllerDict[wizard] as KeyboardInfo;
		
		if (keyboard != null) {
			system.GetKeyboardInfo("Controller").CopyInput(keyboard);
			return;
		}
		
		JoystickInfo joystick = Instance.WizardControllerDict[wizard] as JoystickInfo;
		
		if (joystick != null) {
			system.GetJoystickInfo("Controller").CopyInput(joystick);
			return;
		}
		
		Logger.LogError("NOSEEE dat controller assigning wizbiz has failed...", wizard, system);
	}
	
	public static void AssignController(Wizardz wizard, ControllerInfo controller) {
		Instance.WizardControllerDict[wizard] = controller;
	}
	
	public static ControllerInfo GetNewController() {
		for (int i = 1; i <= Instance.maxPlayers; i++) {
			Joysticks joystick = (Joysticks)i;
				
			if (Instance.activeJoysticks.Contains(joystick)) {
				continue;
			}
				
			KeyCode[] pressedKeys = InputSystem.GetPressedKeys(InputSystem.GetJoystickKeys(joystick));
			
			if (pressedKeys.Length > 0) {
				Instance.activeJoysticks.Add(joystick);
				return Instance.inputSystem.GetJoystickInfo("Controller" + i);
			}
		}
		
		return null;
	}
}

