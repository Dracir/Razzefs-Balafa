using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class zTest : MonoBehaviourExtended, IInputAxisListener {

	[Toggle] public bool logPressedKeys;
	
	void Update() {
		if (logPressedKeys) {
			List<KeyCode> pressed = new List<KeyCode>();
		
			foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) {
				if (Input.GetKey(key)) {
					pressed.Add(key);
				}
			}
		
			Logger.Log(pressed);
		}
	}

	public void OnAxisInput(AxisInfo axisInfo, float axisValue) {
		Logger.Log(axisInfo, axisValue);
	}
}

