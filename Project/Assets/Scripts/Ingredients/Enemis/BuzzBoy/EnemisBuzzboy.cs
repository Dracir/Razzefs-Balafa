using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBuzzboy : StateLayer {
	
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	
	[SerializeField, PropertyField]
	bool rotating = true;
	public bool Rotating{
		set{
			rotating = value;
			GetComponent<SmoothMove>().enabled = rotating;
			GetComponent<SmoothOscillate>().enabled = rotating;
		}
		get{return rotating;}
	}
	
	public bool stationnary;
	
	public float movementSpeed;
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
