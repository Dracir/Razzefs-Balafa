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
	}
	
	public override void OnUpdate(){
		base.OnUpdate();
		
		Layer.Layer.spriteRenderer.color = Interpolation.smoothStep(staringColor,targetColor,t/timeToTransformToTheEtheralFormOfTheWizard);
		t -= Time.deltaTime;
		if(t <= 0) {
			Layer.SwitchState<CharacterEtheralMoving>();
		}
	}
	
	public override void OnExit() {
		base.OnExit();
		
	}
}
