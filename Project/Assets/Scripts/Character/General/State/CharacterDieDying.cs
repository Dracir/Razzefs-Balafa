using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieDying : State {
	
	public GameObject dyingEmitter;
	public Sprite dyingSprite;
	
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
		SwitchState<CharacterDieMovingToSpawn>();	
	}
}
