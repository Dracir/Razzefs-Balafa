using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class CharacterDetail : MonoBehaviourExtended, IIdentifiable {
	
	[SerializeField, PropertyField] int id;
	public int Id {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
	
	public Color color = Color.white;
}

