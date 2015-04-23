using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellGravityCast : StateLayer {
	
	public GameObject castZone;
	public GameObject gravityWell;
	
	public Transform Cursor {
		get {
			return Layer.cursor;
		}
	}
	
	public InputSystem InputSystem { 
		get { 
			return Layer.inputSystem;
		}
	}
	
	CharacterCast Layer {
		get { return (CharacterCast)layer; }
	}
	
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}

	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.UpdateCursor();
	}
}
