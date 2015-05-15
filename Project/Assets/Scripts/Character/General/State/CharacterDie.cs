using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDie : StateLayer, Recyclable {
	
	bool _spriteRendererCached;
	SpriteRenderer _spriteRenderer;
	public SpriteRenderer spriteRenderer { 
		get { 
			_spriteRenderer = _spriteRendererCached ? _spriteRenderer : GetComponentInChildren<SpriteRenderer>();
			_spriteRendererCached = true;
			return _spriteRenderer;
		}
	}
	
    public CharacterStatus Layer {
    	get { return ((CharacterStatus)layer); }
    }
	
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	[Disable] public Animator animator;
	public GameObject portalGameObject;
	[Disable] public Animator portalAnimator;
	
	public override void OnAwake(){
		base.OnAwake();
		animator = GetComponent<Animator>();
		
		portalAnimator = portalGameObject.GetComponent<Animator>();
		
	}

	
	public void recycle(){
		Layer.Die();
	}
}
