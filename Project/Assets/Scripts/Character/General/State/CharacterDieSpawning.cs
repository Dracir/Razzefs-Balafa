using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieSpawning : State {
	
    CharacterDie Layer {
    	get { return (CharacterDie)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	[Disable] public float t;
	public float spawningtime;
	public int nbTurnToDo;
	
	Vector3 endRotation;
	Transform playerTransform;
	
	public GameObject sparklingAnimationPrefab;
	
	
	public override void OnEnter() {
		base.OnEnter();
		playerTransform = Layer.Layer.spriteRenderer.transform;
		Layer.Layer.spriteRenderer.color = new Color(1,1,1,1f);
		playerTransform.rotation = Quaternion.Euler(0,0,0);
		GameObjectExtend.createClone(sparklingAnimationPrefab,transform,transform.position);
		Layer.portalAnimator.SetTrigger("FermeToi");
		t = 0;
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		t+= Time.deltaTime;
		if(t >= spawningtime){		
			SwitchState<CharacterDieIdle>();
			Layer.Layer.SwitchState<CharacterLive>();
		}
		
		playerTransform.localScale = ProLerp.smoothStep(Vector3.zero, Vector3.one, t/spawningtime);
		float rotation = ProLerp.smoothStep(0, nbTurnToDo * 360, t/spawningtime);
		playerTransform.rotation = Quaternion.Euler(0,0,rotation);
	}
	
	public override void OnExit() {
		base.OnExit();
		CharacterStatus character = Layer.Layer;
		character.rigidBody.isKinematic = false;
		character.setColliders(true);
		character.animator.enabled = true;
		playerTransform.rotation = Quaternion.Euler(0,0,0);
		character.animator.enabled = true;
		Layer.portalGameObject.SetActive(false);
	}
}
