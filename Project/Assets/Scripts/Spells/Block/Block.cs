using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Block : MonoBehaviourExtended {

	[SerializeField, Disable] int size;
	public int Size {
		get {
			return size;
		}
		set {
			size = value;
			GetComponent<Rigidbody2D>().mass = 3.Pow(size);
			transform.FindChild("Sprite").SetLocalScale(size, Axis.XY);
		}
	}
	
	public int Area {
		get {
			return size.Pow(2);
		}
	}
}

