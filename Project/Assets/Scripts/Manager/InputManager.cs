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
    
	void Awake() {
		DontDestroyOnLoad(this);
	}
	
	public static void SetController(Wizardz wizard, InputSystem system) {
		if (!Instance.WizardControllerDict.ContainsKey(wizard)) {
			return;
		}
		
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
		JoystickInfo joystick = controller as JoystickInfo;
		
		if (joystick != null) {
			Instance.activeJoysticks.Add(joystick.Joystick);
		}
		
		Instance.WizardControllerDict[wizard] = controller;
	}
	
	public static void ReleaseController(Wizardz wizard) {
		JoystickInfo joystick = Instance.WizardControllerDict[wizard] as JoystickInfo;
		
		if (joystick != null) {
			Instance.activeJoysticks.Remove(joystick.Joystick);
		}
		
		Instance.WizardControllerDict.Remove(wizard);
	}
	
	public static void SwitchController(Wizardz wizard, Wizardz otherWizard) {
		if (Instance.WizardControllerDict.ContainsKey(wizard)) {
			Instance.WizardControllerDict[otherWizard] = Instance.WizardControllerDict[wizard];
			Instance.WizardControllerDict.Remove(wizard);
		}
	}
	
	public static ControllerInfo GetNewController() {
		for (int i = 1; i <= 8; i++) {
			Joysticks joystick = (Joysticks)i;
				
			if (Instance.activeJoysticks.Contains(joystick)) {
				continue;
			}
				
			KeyCode[] pressedKeys = InputSystem.GetKeysDown(InputSystem.GetJoystickKeys(joystick));
			
			if (pressedKeys.Length > 0) {
				return Instance.inputSystem.GetJoystickInfo("Controller" + i);
			}
		}
		
		return null;
	}
}

