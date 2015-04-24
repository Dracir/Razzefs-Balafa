using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellRainCast : StateLayer {
	
	public GameObject castZone;
	public GameObject rain;
	
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
	
	public override void OnExit() {
		base.OnExit();
		
		SwitchState<SpellRainCastIdle>();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.UpdateCursor();
	}
}
