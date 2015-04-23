﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class Temperature : MonoBehaviourExtended {

	[Range(-1, 1)] public float temperature;
	[Range(-1, 0)] public float freezingThreshold = 0.75F;
	[Range(0, 1)] public float blazingThreshold = 0.75F;
	[Min] public float resistance = 1;
	
	public bool IsFreezing {
		get {
			return temperature <= freezingThreshold;
		}
	}
	
	public bool IsBlazing {
		get {
			return temperature >= blazingThreshold;
		}
	}
	
	public float AmbientTemperature {
		get {
			return 0;
		}
	}
	
	void Update() {
		float difference = AmbientTemperature - temperature;
		float increment = difference * Time.deltaTime / resistance;
		
		if (increment > difference) {
			temperature += increment;
		}
		else {
			temperature = AmbientTemperature;
		}
	}
}

