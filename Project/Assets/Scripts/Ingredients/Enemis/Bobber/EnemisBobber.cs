using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobber : StateLayer {

    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public float movementSpeed = 5;
	public float activationTime = 1;
	public float explosionRadius = 1.5f;
	
	public LayerMask activationLayers;
	public float maxHeatDamage = 1;
	
	[HideInInspector] public Temperature temperature;
	
	public override void OnAwake() {
		base.OnAwake();
		temperature = GetComponent<Temperature>();
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
	}
}
