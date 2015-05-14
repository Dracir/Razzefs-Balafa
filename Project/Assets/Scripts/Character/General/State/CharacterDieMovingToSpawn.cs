using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieMovingToSpawn : State {
	
	[Disable] public Vector3 startPosition;
	[Disable] public Vector3 endPosition;
	[Disable] public float respawntimer;
	
    CharacterDie Layer {
    	get { return (CharacterDie)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		Layer.Layer.spriteRenderer.color = new Color(1f,1f,1f,0f);
		startPosition = transform.position;
		respawntimer = 0;
		endPosition = GameObject.Find("P1Start").transform.position;
	}
	
	public override void OnExit() {
		base.OnExit();
		transform.position = endPosition;
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		respawntimer += Time.deltaTime;
		/*float distance = (startPosition - endPosition).magnitude;
		float timeToReachRespawn = Mathf.Lerp(Layer.Layer.minRespawnTime, Layer.Layer.maxRespawnTime, distance / Layer.Layer.maxDistance);

		float t = (respawntimer / timeToReachRespawn);
		
		float tmod = t*t * (3f - 2f*t);
		transform.position = Vector3.Lerp(startPosition,endPosition, tmod);*/
		
		if(respawntimer >= Layer.Layer.RespawnTime){
			SwitchState<CharacterDieWaitingToRevive>();
		}
	}
}
