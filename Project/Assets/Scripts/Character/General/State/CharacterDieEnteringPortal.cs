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
		Layer.portalGameObject.transform.position = transform.position;
		Layer.portalAnimator.SetTrigger("OuvreToi");
		Layer.portalGameObject.GetComponent<SpriteRenderer>().color = Layer.Layer.detail.color;
		t = Layer.portalAnimator.GetCurrentAnimatorStateInfo(0).length;
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		
		Layer.Layer.spriteRenderer.transform.localScale = Interpolation.smoothStep(Vector3.one, Vector3.zero, t/enterTime);
		
		t -= Time.deltaTime;
		if (Layer.portalAnimator.isPlaying()){
			SwitchState<CharacterDieSpawning>();
		}
			
		/*if(t <= 0){
			SwitchState<CharacterDieSpawning>();
		}*/
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
