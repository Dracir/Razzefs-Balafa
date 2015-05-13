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
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
	
	void Start() {
		if (!jTenTrainDeDebugger) {
			InputManager.SetController(wizard, inputSystem);
		}
	}
}

