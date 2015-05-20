using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Magicolo;
using DG.Tweening;

public class Thermometer : MonoBehaviourExtended {

	public Slider preHotSlider;
	public Slider hotSlider;
	public Slider preColdSlider;
	public Slider coldSlider;
	public Image[] imagesToFade = new Image[0];
	[Min] public float fillSpeed = 2;
	[Min] public float fillDelay = 0.5F;
	[Min] public float activeAlpha = 0.75F;
	[Min] public float activeFadeSpeed = 2;
	[Min] public float activeThreshold = 0.05F;
	[Min] public float inactiveAlpha = 0.1F;
	[Min] public float inactiveFadeSpeed = 0.25F;
	[Min] public float inactiveDelay = 2;
	
	float temperature = 0;
	public float Temperature {
		get { 
			return temperature; 
		}
		set {
			Hotness = Mathf.Clamp(value, 0, 1);
			Coldness = Mathf.Abs(Mathf.Clamp(value, -1, 0));
			
			if ((value == 0 && temperature != 0) || Mathf.Abs(temperature - value) > activeThreshold) {
				temperature = value;
			
				UdpateTemperature();
			}
		}
	}
	
	float hotness;
	public float Hotness {
		get { 
			return hotness; 
		}
		set { 
			hotness = value; 
			preHotSlider.value = hotness;
		}
	}
	
	float coldness;
	public float Coldness {
		get { 
			return coldness; 
		}
		set { 
			coldness = value; 
			preColdSlider.value = coldness;
		}
	}
	
	void Awake() {
		foreach (Image image in imagesToFade) {
			image.DOFade(inactiveAlpha, 0);
		}
		
		preHotSlider.value = 0;
		hotSlider.value = 0;
		preColdSlider.value = 0;
		coldSlider.value = 0;
	}
	
	void UdpateTemperature() {
		hotSlider.DOKill();
		hotSlider.DOValue(hotness, 1F / fillSpeed).SetDelay(fillDelay);
		coldSlider.DOKill();
		coldSlider.DOValue(coldness, 1F / fillSpeed).SetDelay(fillDelay);
		
		foreach (Image image in imagesToFade) {
			image.DOKill();
			image.DOFade(activeAlpha, 1F / activeFadeSpeed);
			image.DOFade(inactiveAlpha, 1F / inactiveFadeSpeed).SetDelay(inactiveDelay);
		}
	}
}

