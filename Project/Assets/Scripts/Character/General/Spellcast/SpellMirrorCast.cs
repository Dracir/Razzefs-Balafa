using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class SpellMirrorCast : StateLayer {
	
	[Min] public float minCursorRange = 2;
	[Min] public float maxCursorRange = 2;
	public GameObject castZone;
	public GameObject mirror;
	
	public Transform Cursor {
		get {
			return Layer.cursor;
		}
	}
	
	public Vector2 CursorOffset {
		get {
			return Layer.cursorOffset;
		}
		set {
			Layer.cursorOffset = value;
		}
	}
	
	public Vector2 WorldCursorTarget {
		get {
			return Layer.targetPosition + (Vector2)transform.position;
		}
	}
	
	public Vector2 LocalCursorTarget {
		get {
			return Layer.targetPosition;
		}
		set {
			Layer.targetPosition = value;
		}
	}
	
	public float MinCursorRange {
		get {
			return Layer.minCursorRange;
		}
		set {
			Layer.minCursorRange = value;
		}
	}
	
	public float MaxCursorRange {
		get {
			return Layer.maxCursorRange;
		}
		set {
			Layer.maxCursorRange = value;
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
	
	public override void OnEnter() {
		base.OnEnter();
		
		Layer.minCursorRange = minCursorRange;
		Layer.maxCursorRange = maxCursorRange;
	}
	
	public override void OnExit() {
		base.OnExit();
		
		SwitchState<SpellMirrorCastIdle>();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.UpdateCursor();
	}
}
