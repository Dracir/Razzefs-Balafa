﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public class TemperatureInfo : MonoBehaviourExtended {

	[SerializeField, PropertyField(typeof(RangeAttribute), -1, 1)]
	float temperature;
	public float Temperature {
		get {
			return temperature;
		}
		set {
			temperature = Mathf.Clamp(value, -1, 1);
		}
	}
	
	[Range(-1, 0)] public float freezingThreshold = -0.5F;
	[Range(0, 1)] public float blazingThreshold = 0.5F;
	[Min] public float resistance = 1;
	
	[Disable] public bool wasFrozen;
	[Disable] public bool wasBlazed;
	
	public float Coldness {
		get {
			return IsFreezing ? (Temperature - freezingThreshold) / (-1 - freezingThreshold) : 0;
		}
	}
	
	public float Hotness {
		get {
			return IsBlazing ? (Temperature - blazingThreshold) / (1 - blazingThreshold) : 0;
		}
	}
	
	public bool IsCool {
		get {
			return Temperature < 0 && Temperature > freezingThreshold;
		}
	}
	
	public bool IsWarm {
		get {
			return Temperature > 0 && Temperature < blazingThreshold;
		}
	}
	
	public bool IsFreezing {
		get {
			return Temperature <= freezingThreshold;
		}
	}
	
	public bool IsBlazing {
		get {
			return Temperature >= blazingThreshold;
		}
	}
	
	public bool IsHot{
		get{
			return Temperature > 0;
		}
	}
	
	public float AmbientTemperature {
		get {
			return 0;
		}
	}
	
	void Update() {
		wasFrozen |= Temperature <= -1;
		wasBlazed |= Temperature >= 1;
			
		float difference = AmbientTemperature - Temperature;
		float increment = difference == 0 ? 0 : difference.Sign() * Time.deltaTime / resistance;
		
		if (Mathf.Abs(difference) > Mathf.Abs(increment)) {
			Temperature += increment;
		}
		else {
			Temperature = AmbientTemperature;
		}
	}
}
