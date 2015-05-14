using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieDying : State {
	
	public GameObject dyingEmitter;
	public Sprite dyingSprite;
	
	float t=0;
	Color baseColor = new Color(1f,1f,1f,0f);
	Color finalColor = new Color(1f,1f,1f,0.5f);
	
    CharacterDie Layer {
    	get { return (CharacterDie)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	public override void OnEnter() {
		base.OnEnter();
		CharacterStatus character = Layer.Layer;
		character.spriteRenderer.color = new Color(0,0,0,0f);
		character.spriteRenderer.sprite = dyingSprite;
		character.rigidBody.isKinematic = true;
		character.setColliders(false);
		character.animator.enabled = false;
		GameObjectExtend.createClone(dyingEmitter, transform, transform.position);
		StartCoroutine("WaitToMove");
		t = 0;
		
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.Layer.spriteRenderer.color = Color.Lerp(baseColor,finalColor, t / Layer.Layer.timeBeforeSpiritLeaveCorpse);
		t += Time.deltaTime;
	}
	
	IEnumerator WaitToMove(){
		yield return new WaitForSeconds(Layer.Layer.timeBeforeSpiritLeaveCorpse);
		SwitchState<CharacterDieMovingToSpawn>();
	}
}
