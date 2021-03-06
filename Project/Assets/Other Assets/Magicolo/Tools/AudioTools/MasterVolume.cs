﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Magicolo;

public enum MasterBuses {
	Music,
	SFX,
	Voice
}

public class MasterVolume : MonoBehaviour {
	
	static bool instanceCached;
	static MasterVolume instance;
	static MasterVolume Instance {
		get {
			if (!instanceCached) {
				instance = FindObjectOfType<MasterVolume>();
				instanceCached = true;
			}
			
			return instance;
		}
		set {
			instance = value;
			instanceCached = true;
		}
	}
	
	[SerializeField, PropertyField]
	AudioMixer masterMixer;
	public AudioMixer MasterMixer {
		get {
			return masterMixer;
		}
		set {
			masterMixer = value;
			SetMixerToBuses();
		}
	}
	
	[SerializeField, Empty(DisableBool = "MasterMixerNotNull", BeforeSeparator = true)]
	MasterAudioBus music = null;
	public static MasterAudioBus Music {
		get {
			return Instance.music;
		}
	}
	
	[SerializeField, Empty(DisableBool = "MasterMixerNotNull")]
	MasterAudioBus sfx = null;
	public static MasterAudioBus SFX {
		get {
			return Instance.sfx;
		}
	}
	
	[SerializeField, Empty(DisableBool = "MasterMixerNotNull")]
	MasterAudioBus voice = null;
	public static MasterAudioBus Voice {
		get {
			return Instance.voice;
		}
	}
	
	bool MasterMixerNotNull {
		get {
			return MasterMixer == null;
		}
	}
	
	public void SetMusicVolume(float volume) {
		Music.Volume = volume;
	}
	
	public void SetSFXVolume(float volume) {
		SFX.Volume = volume;
	}
	
	public void SetVoiceVolume(float volume) {
		Voice.Volume = volume;
	}
	
	void Start() {
		if (IsSingleton()) {
			Instance = this;
			DontDestroyOnLoad(this);
			InitializeMixer();
		}
		else {
			this.Remove();
		}
	}
	
	void InitializeMixer() {
		foreach (MasterBuses bus in Enum.GetValues(typeof(MasterBuses))) {
			MasterAudioBus audioBus = BusToAudioBus(bus);
			audioBus.Initialize();
		}
	}

	void SetMixerToBuses() {
		foreach (MasterBuses bus in Enum.GetValues(typeof(MasterBuses))) {
			BusToAudioBus(bus).Mixer = MasterMixer;
		}
	}
	
	bool IsSingleton() {
		return Instance == null || Instance == this;
	}

	static MasterAudioBus BusToAudioBus(MasterBuses bus) {
		switch (bus) {
			case MasterBuses.Music:
				return Music;
			case MasterBuses.SFX:
				return SFX;
			case MasterBuses.Voice:
				return Voice;
			default:
				return Music;
		}
	}
}
