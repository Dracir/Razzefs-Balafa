using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Rick;

public class CharacterDieEnteringPortal : State {
	
    CharacterDie Layer {
    	get { return (CharacterDie)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	[Disable] public float t;
	public float enterTime;
	
	public override void OnEnter() {
		base.OnEnter();
		Layer.portalGameObject.SetActive(true);
		Layer.portalAnimator.SetTrigger("ouvreToi");
		t = 0;
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		Layer.Layer.spriteRenderer.transform.localScale = ProLerp.smoothStep(Vector3.one, Vector3.zero, t/enterTime);
		
		t+= Time.deltaTime;
		if(t >= enterTime){
			SwitchState<CharacterDieSpawning>();
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
