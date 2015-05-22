using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterEtheralTransforming : State {
	
    CharacterEtheral Layer {
    	get { return (CharacterEtheral)layer; }
    }
    
    StateMachine Machine {
    	get { return (StateMachine)machine; }
    }
	
	[Disable] public float t = 0;
	public float timeToTransformToTheEtheralFormOfTheWizard;
	
	[Disable] public Color staringColor;
	public Color targetColor;
	
	public override void OnEnter() {
		base.OnEnter();
		t = timeToTransformToTheEtheralFormOfTheWizard;
		staringColor = Layer.Layer.spriteRenderer.color;
		
		CharacterStatus character = Layer.Layer;
		character.spriteRenderer.color = new Color(1,1,1,1f);
		character.spriteRenderer.sprite = Layer.Layer.holdFlyingSprite;
		character.rigidBody.isKinematic = true;
		character.setColliders(false);
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		
		Layer.Layer.spriteRenderer.color = Interpolation.smoothStep(targetColor,staringColor,t/timeToTransformToTheEtheralFormOfTheWizard);
		t -= Time.deltaTime;
		if(t <= 0) {
			Layer.SwitchState<CharacterEtheralMoving>();
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
