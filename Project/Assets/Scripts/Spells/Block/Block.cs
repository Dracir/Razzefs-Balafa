﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Block : MonoBehaviourExtended, Recyclable {

	bool _spriteTransformCached;
	Transform _spriteTransform;
	public Transform spriteTransform { 
		get { 
			_spriteTransform = _spriteTransformCached ? _spriteTransform : transform.FindChild("Sprite");
			_spriteTransformCached = true;
			return _spriteTransform;
		}
	}
	
	bool _spriteAnimatorCached;
	Animator _spriteAnimator;
	public Animator spriteAnimator { 
		get { 
			_spriteAnimator = _spriteAnimatorCached ? _spriteAnimator : this.FindComponent<Animator>();
			_spriteAnimatorCached = true;
			return _spriteAnimator;
		}
	}
	
	[Disable] public int sizeHash = Animator.StringToHash("Size");
	
	[SerializeField, Disable] int size;
	public int Size {
		get {
			return size;
		}
		set {
			size = value;
			GetComponent<Rigidbody2D>().mass = 2.Pow(size);
			spriteTransform.SetLocalScale(size, Axis.XY);
			spriteAnimator.SetFloat(sizeHash, value);
		}
	}
	
	public int Area {
		get {
			return size.Pow(2);
		}
	}

	public void Explode() {
		gameObject.Remove();
	}
	
	public void recycle() {
		if (size == 1) {
			Explode();
		}
		else {
			// TODO HP ?
		}
	}
}

