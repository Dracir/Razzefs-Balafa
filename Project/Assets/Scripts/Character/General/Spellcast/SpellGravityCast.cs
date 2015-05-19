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
	
	public TemperatureInfo TemperatureInfo {
		get {
			return Layer.temperatureInfo;
		}
	}
		
	public AudioPlayer AudioPlayer {
		get {
			return Layer.audioPlayer;
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
		
		SwitchState<SpellGravityCastIdle>();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.UpdateCursor();
	}
}
