using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class HarryCastCasting : State {
	
	public ParticleSystem particleFX;
	
	HarryCast Layer {
		get { return ((HarryCast)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnEnter() {
		base.OnEnter();
		
		Cast();
		SwitchState<HarryCastIdle>();
	}

	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.UpdateCursor();
	}
	
	void Cast() {
		if (particleFX != null) {
			particleFX.transform.localPosition = Layer.targetPosition;
			particleFX.Play();
		}
	}
}
