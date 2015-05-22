using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class zTest : MonoBehaviourExtended {

	[Button("Test", "Test", NoPrefixLabel = true)] public bool test;
	void Test() {
		
	}
	
	[Toggle] public bool logPressedKeys;
	[Toggle] public bool setKey;
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}

	void Update() {
		if (logPressedKeys) {
			Logger.Log(InputSystem.GetKeysPressed());
		}
	}
	
	void OnTransformParentChanged() {
		Logger.Log("DONT TOUCH ME!!!");
	}
}
