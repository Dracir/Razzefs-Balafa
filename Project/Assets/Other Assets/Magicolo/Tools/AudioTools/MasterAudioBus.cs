using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using Magicolo;

namespace Magicolo {
	[System.Serializable]
	public class MasterAudioBus {

		[SerializeField, PropertyField]
		string parameter = "Volume";
		public string Parameter {
			get {
				return parameter;
			}
			set {
				parameter = value;
			}
		}
	
		[SerializeField, PropertyField(typeof(RangeAttribute), -80, 0)]
		float volume;
		public float Volume {
			get {
				return volume;
			}
			set {
				volume = value;
				
				PlayerPrefs.SetFloat(Parameter, volume);
				Mixer.SetFloat(Parameter, volume);
				
				if (Slider != null) {
					Slider.value = volume;
				}
			}
		}
	
		[SerializeField, PropertyField]
		Slider slider;
		public Slider Slider {
			get {
				return slider;
			}
			set {
				slider = value;
				slider.value = Volume;
			}
		}
		
		[SerializeField, HideInInspector]
		AudioMixer mixer;
		public AudioMixer Mixer {
			get {
				return mixer;
			}
			set {
				mixer = value;
			}
		}
	
		public void Initialize() {
			Volume = PlayerPrefs.GetFloat(Parameter, 0);
		}
	}
}