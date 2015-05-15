using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TemperatureDisplay : MonoBehaviour {
	
	Slider slider;
	void Awake () {
		slider = GetComponent<Slider>();
	}
	void Start () {
		
	}
	
	void Update () {
		
	}
	
	public void ShowTemp (float value){
		slider.value = value;
	}
	
}
