using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class InputManager : MonoBehaviourExtended {

	public InputSystem[] playerControllers;
	[Disable] public bool keyboardDispatched;
	[Disable] public int activePlayers;
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
    
	void Start() {
		StartCoroutine(ListenToNewControllers());
	}
	
	public void DispatchKeyboard(InputSystem system) {
		system.GetKeyboardInfo("Controller").CopyInput(inputSystem.GetKeyboardInfo("Controller"));
		
		keyboardDispatched = true;
		activePlayers += 1;
	}
	
	public void DispatchJoystick(InputSystem system, Joysticks joystick) {
		JoystickInfo info = system.GetJoystickInfo("Controller");
		
		info.CopyInput(inputSystem.GetJoystickInfo("Controller" + activePlayers));
		info.Joystick = joystick;
		
		activeJoysticks.Add(joystick);
		activePlayers += 1;
	}
	
	IEnumerator ListenToNewControllers() {
		while (activePlayers < playerControllers.Length) {
			KeyCode[] pressedKeys;
			
			if (!keyboardDispatched) {
				pressedKeys = InputSystem.GetPressedKeys(InputSystem.GetKeyboardKeys());
			
				if (pressedKeys.Length > 0) {
					DispatchKeyboard(playerControllers[activePlayers]);
					continue;
				}
			}
			
			for (int i = 1; i <= playerControllers.Length; i++) {
				Joysticks joystick = (Joysticks)i;
				
				if (activeJoysticks.Contains(joystick)) {
					continue;
				}
				
				pressedKeys = InputSystem.GetPressedKeys(InputSystem.GetJoystickKeys(joystick));
			
				if (pressedKeys.Length > 0) {
					if (!activeJoysticks.Contains(joystick)) {
						DispatchJoystick(playerControllers[activePlayers], joystick);
						continue;
					}
				}
			}
			
			yield return new WaitForSeconds(0);
		}
	}
}

