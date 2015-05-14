using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisSofterFiring : State {
	
	public LineRenderer[] lineRenderers;
	public BoxCollider2D laserTrigger;
	public TotalEradicationRay totalEradicationRay;
	
    EnemisSofter Layer {
    	get { return (EnemisSofter)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		totalEradicationRay.enabled = true;
		totalEradicationRay.setLazerActive(true);
	}
	
	public override void OnExit() {
		base.OnExit();
		totalEradicationRay.enabled = false;
		totalEradicationRay.setLazerActive(false);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
	}
}
