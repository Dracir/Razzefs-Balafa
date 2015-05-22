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
	
	[Disable] public List<ControllerInfo> activeControllers = new List<ControllerInfo>();
	
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
		instance = this;
	}
	
	void Start() {
		if (Instance != null && Instance != this) {
			gameObject.Remove();
		}
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
			system.GetJoystickInfo("Controller").Joystick = joystick.Joystick;
			return;
		}
		
		Logger.LogError("NOSEEE dat controller assigning wizbiz has failed...", wizard, system);
	}
	
	public static void AssignController(Wizardz wizard, ControllerInfo controller) {
		Instance.activeControllers.Add(controller);
		Instance.WizardControllerDict[wizard] = controller;
	}
	
	public static void ReleaseController(Wizardz wizard) {
		Instance.activeControllers.Remove(Instance.WizardControllerDict[wizard]);
		Instance.WizardControllerDict.Remove(wizard);
	}
	
	public static void SwitchController(Wizardz wizard, Wizardz otherWizard) {
		if (Instance.WizardControllerDict.ContainsKey(wizard)) {
			Instance.WizardControllerDict[otherWizard] = Instance.WizardControllerDict[wizard];
			Instance.WizardControllerDict.Remove(wizard);
		}
	}
	
	public static ControllerInfo CheckForNewController() {
		ControllerInfo controller = null;
		
		if (InputSystem.GetKeysDown(InputSystem.GetKeyboardKeys()).Length > 0) {
			KeyboardInfo keyboard = Instance.inputSystem.GetKeyboardInfo("Controller");
			
			if (!Instance.activeControllers.Contains(keyboard)) {
				controller = keyboard;
			}
		}
		
		for (int i = 1; i < 9; i++) {
			if (InputSystem.GetKeysDown(InputSystem.GetJoystickKeys((Joysticks)i)).Length > 0) {
				JoystickInfo joystick = Instance.inputSystem.GetJoystickInfo("Controller" + i);
				
				if (!Instance.activeControllers.Contains(joystick)) {
					controller = joystick;
				}
			}
		}
		
		return controller;
	}
}

