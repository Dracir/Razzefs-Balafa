using DG.Tweening;
using UnityEngine;
using System.Collections;
using Magicolo;

public class TemperatureDecorator : MonoBehaviour {

	public SpriteRenderer[] spriteRenderers;
	TemperatureInfo temperatureInfo;
	
	public float fadeSpeed = 3;
	
	public GameObject frozenEffectPrefab;
	public Color frozenColor = Color.blue;
	[Disable]public GameObject frozenSystem;
	
	public GameObject blazedEffectPrefab;
	public Color blazedColor = Color.red;
	[Disable]public GameObject blazingSystem;
	
	[Disable]public  int currentTemperatureMod = 0;
	
	
	void Start () {
		temperatureInfo = GetComponent<TemperatureInfo>();
		if(frozenEffectPrefab != null){
			frozenSystem = GameObjectExtend.createClone(frozenEffectPrefab,transform, transform.position);
			frozenSystem.transform.Translate(new Vector3(0,0,-0.1f));
			frozenSystem.gameObject.SetActive(false);
		}
		if(blazedEffectPrefab != null){
			blazingSystem = GameObjectExtend.createClone(blazedEffectPrefab,transform, transform.position);
			blazingSystem.transform.Translate(new Vector3(0,0,-0.1f));
			blazingSystem.SetActive(false);
		}
	}
	
	
	void Update () {
		switch(currentTemperatureMod){
				case -1 : handleFrozen(); break;
				case  0 : handleNeutral(); break;
				case  1 : handleBlazed(); break;
		}
	}

	void handleBlazed() {
		if(!temperatureInfo.IsBlazing){
			switchToNeutral();
		}else{
			Color targetColor = temperatureInfo.IsBlazing ? blazedColor :  new Color(1, 1 - temperatureInfo.Hotness, 1 - temperatureInfo.Coldness);
			lerpColorsTo(targetColor);
		}
		
	}

	void handleFrozen() {
		if(!temperatureInfo.IsFreezing){
			switchToNeutral();
		}else{
			Color targetColor = temperatureInfo.IsFreezing ? frozenColor : new Color(1 - temperatureInfo.Coldness, 1 - temperatureInfo.Coldness, 1);
			lerpColorsTo(targetColor);
		}
	}

	void handleNeutral() {
		if(temperatureInfo.IsBlazing){
			switchToBlazed();
		}else if(temperatureInfo.IsFreezing){
			switchToFrozen();
		}
	}

	void switchToBlazed() {
		currentTemperatureMod = 1;
		if(blazingSystem != null) blazingSystem.SetActive(true);
	}

	void switchToFrozen() {
		currentTemperatureMod = -1;
		if(frozenSystem != null) frozenSystem.SetActive(true);
	}

	void switchToNeutral() {
		currentTemperatureMod = 0;
		changeColorsTo( Color.white );
		if(blazingSystem != null) blazingSystem.SetActive(false);
		if(frozenSystem != null) frozenSystem.SetActive(false);
	}
	
	void changeColorsTo(Color color){
		foreach (var spriteRenderer in spriteRenderers) {
			spriteRenderer.color = color;
		}
	}
	
	void lerpColorsTo(Color color){
		foreach (var spriteRenderer in spriteRenderers) {
			spriteRenderer.color = spriteRenderer.color.Lerp(color, Time.deltaTime * fadeSpeed, Channels.RGB);
		}
	}
}
