using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class EnemisBobberActivating : State {
	
	EnemisBobber Layer {
		get { return (EnemisBobber)layer; }
	}
    
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
	
	public float t;
	
	public override void OnEnter() {
		base.OnEnter();
		t = Layer.activationTime;
		GetComponent<Animator>().SetTrigger("Activating");
		GetComponent<CircleCollider2D>().enabled = false;
		
		Layer.audioPlayer.Play("Scream");
		Layer.audioPlayer.Play("Beep");
		Layer.audioPlayer.Play("Beep", 0.15F);
		Layer.audioPlayer.Play("Beep", 0.3F);
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		t -= Time.deltaTime;
		if (t <= 0) {
			SwitchState<EnemisBobberExplosing>();
		}
	}
}
