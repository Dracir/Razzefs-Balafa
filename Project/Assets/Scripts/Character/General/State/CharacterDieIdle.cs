using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDieIdle : State {
	
	public float fadeSpeed = 5;
	
	CharacterDie Layer {
		get { return ((CharacterDie)layer); }
	}
    
	StateMachine Machine {
		get { return ((StateMachine)machine); }
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		
		Layer.spriteRenderer.FadeTowards(0, fadeSpeed, Channels.A);
		
		if (Layer.spriteRenderer.color.a <= 0) {
			Destroy(gameObject);
		}
	}
}
