using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using Magicolo;
using Magicolo.AudioTools;

/// <summary>
/// Lets you override the settings of a sound set in the inspector.
/// </summary>
[System.Serializable]
public class AudioOption {

	public enum OptionTypes {
		Volume,
		Pitch,
		RandomVolume,
		RandomPitch,
		FadeIn,
		FadeOut,
		Loop,
		Clip,
		Output,
		DopplerLevel,
		RolloffMode,
		MinDistance,
		MaxDistance,
		Spread,
		Mute,
		BypassEffects,
		BypassListenerEffects,
		BypassReverbZones,
		Priority,
		StereoPan,
		SpatialBlend,
		ReverbZoneMix,
		Time,
		TimeSamples,
		VelocityUpdateMode,
		IgnoreListenerPause,
		IgnoreListenerVolume
	}
	
	public OptionTypes type;
	public AudioOptionValue value;
	public float delay;
	
	AudioOption(OptionTypes type, object value, object defaultValue, float delay = 0) {
		this.type = type;
		this.value = new AudioOptionValue(value, defaultValue);
		this.delay = delay;
	}

	public static AudioOption Clip(AudioClip clip, float delay) {
		return new AudioOption(OptionTypes.Clip, clip, null, delay);
	}
	
	public static AudioOption Clip(AudioClip clip) {
		return Clip(clip, 0);
	}
	
	public static AudioOption Output(AudioMixerGroup mixerGroup, float delay) {
		return new AudioOption(OptionTypes.Output, mixerGroup, null, delay);
	}
	
	public static AudioOption Output(AudioMixerGroup mixerGroup) {
		return Output(mixerGroup, 0);
	}
	
	public static AudioOption FadeIn(float fadeIn, float delay) {
		return new AudioOption(OptionTypes.FadeIn, fadeIn, delay);
	}
	
	public static AudioOption FadeIn(float fadeIn) {
		return FadeIn(fadeIn, 0);
	}
	
	public static AudioOption FadeOut(float fadeOut, float delay) {
		return new AudioOption(OptionTypes.FadeOut, fadeOut, delay);
	}
	
	public static AudioOption FadeOut(float fadeOut) {
		return FadeOut(fadeOut, 0);
	}
	
	public static AudioOption Volume(float volume, float time, Ease ease, float delay) {
		return new AudioOption(OptionTypes.Volume, new []{ volume, time, (float)ease }, new []{ 1F, 0F }, delay);
	}
	
	public static AudioOption Volume(float volume, float time) {
		return Volume(volume, time, Ease.Linear, 0);
	}
	
	public static AudioOption Volume(float volume) {
		return Volume(volume, 0, Ease.Linear, 0);
	}
	
	public static AudioOption Pitch(float pitch, float time, Ease ease, float delay) {
		return new AudioOption(OptionTypes.Pitch, new []{ pitch, time, (float)ease }, new []{ 1F, 0F }, delay);
	}
	
	public static AudioOption Pitch(float pitch, float time, Ease ease) {
		return Pitch(pitch, time, ease, 0);
	}
	
	public static AudioOption Pitch(float pitch, float time) {
		return Pitch(pitch, time, Ease.Linear, 0);
	}
	
	public static AudioOption Pitch(float pitch) {
		return Pitch(pitch, 0, Ease.Linear, 0);
	}
	
	public static AudioOption RandomVolume(float randomVolume, float delay) {
		return new AudioOption(OptionTypes.RandomVolume, randomVolume, 0, delay);
	}
	
	public static AudioOption RandomVolume(float randomVolume) {
		return RandomVolume(randomVolume, 0);
	}
	
	public static AudioOption RandomPitch(float randomPitch, float delay) {
		return new AudioOption(OptionTypes.RandomPitch, randomPitch, 0, delay);
	}
	
	public static AudioOption RandomPitch(float randomPitch) {
		return RandomPitch(randomPitch, 0);
	}
	
	public static AudioOption Loop(bool loop, float delay) {
		return new AudioOption(OptionTypes.Loop, loop, false, delay);
	}
	
	public static AudioOption Loop(bool loop) {
		return Loop(loop, 0);
	}
	
	public static AudioOption DopplerLevel(float dopplerLevel, float delay) {
		return new AudioOption(OptionTypes.DopplerLevel, dopplerLevel, 1, delay);
	}
	
	public static AudioOption DopplerLevel(float dopplerLevel) {
		return DopplerLevel(dopplerLevel, 0);
	}
	
	public static AudioOption RolloffMode(AudioRolloffMode rolloffMode, float delay) {
		return new AudioOption(OptionTypes.RolloffMode, rolloffMode, (float)AudioRolloffMode.Logarithmic, delay);
	}
	
	public static AudioOption RolloffMode(AudioRolloffMode rolloffMode) {
		return RolloffMode(rolloffMode, 0);
	}
	
	public static AudioOption MinDistance(float minDistance, float delay) {
		return new AudioOption(OptionTypes.MinDistance, minDistance, 5, delay);
	}
	
	public static AudioOption MinDistance(float minDistance) {
		return MinDistance(minDistance, 0);
	}
	
	public static AudioOption MaxDistance(float maxDistance, float delay) {
		return new AudioOption(OptionTypes.MaxDistance, maxDistance, 500, delay);
	}
	
	public static AudioOption MaxDistance(float maxDistance) {
		return MaxDistance(maxDistance, 0);
	}
	
	public static AudioOption Spread(float panLevel, float delay) {
		return new AudioOption(OptionTypes.Spread, panLevel, 1, delay);
	}
		
	public static AudioOption Spread(float panLevel) {
		return Spread(panLevel, 0);
	}
		
	public static AudioOption Mute(bool mute, float delay) {
		return new AudioOption(OptionTypes.Mute, mute, false, delay);
	}
	
	public static AudioOption Mute(bool mute) {
		return Mute(mute, 0);
	}
	
	public static AudioOption BypassEffects(bool bypassEffects, float delay) {
		return new AudioOption(OptionTypes.BypassEffects, bypassEffects, false, delay);
	}
	
	public static AudioOption BypassEffects(bool bypassEffects) {
		return BypassEffects(bypassEffects, 0);
	}
	
	public static AudioOption BypassListenerEffects(bool bypassListenerEffects, float delay) {
		return new AudioOption(OptionTypes.BypassListenerEffects, bypassListenerEffects, false, delay);
	}
	
	public static AudioOption BypassListenerEffects(bool bypassListenerEffects) {
		return BypassListenerEffects(bypassListenerEffects, 0);
	}
	
	public static AudioOption BypassReverbZones(bool bypassReverbZones, float delay) {
		return new AudioOption(OptionTypes.BypassReverbZones, bypassReverbZones, false, delay);
	}
	
	public static AudioOption BypassReverbZones(bool bypassReverbZones) {
		return BypassReverbZones(bypassReverbZones, 0);
	}
	
	public static AudioOption Priority(int priority, float delay) {
		return new AudioOption(OptionTypes.Priority, (float)priority, 0, delay);
	}
	
	public static AudioOption Priority(int priority) {
		return Priority(priority, 0);
	}
	
	public static AudioOption StereoPan(float stereoPan, float delay) {
		return new AudioOption(OptionTypes.StereoPan, stereoPan, 0, delay);
	}
	
	public static AudioOption StereoPan(float stereoPan) {
		return StereoPan(stereoPan, 0);
	}
	
	public static AudioOption SpatialBlend(float spatialBlend, float delay) {
		return new AudioOption(OptionTypes.SpatialBlend, spatialBlend, 0, delay);
	}
	
	public static AudioOption SpatialBlend(float spatialBlend) {
		return SpatialBlend(spatialBlend, 0);
	}
	
	public static AudioOption ReverbZoneMix(float reverbZoneMix, float delay) {
		return new AudioOption(OptionTypes.ReverbZoneMix, reverbZoneMix, 0, delay);
	}
	
	public static AudioOption ReverbZoneMix(float reverbZoneMix) {
		return ReverbZoneMix(reverbZoneMix, 0);
	}
	
	public static AudioOption Time(float time, float delay) {
		return new AudioOption(OptionTypes.Time, time, 0, delay);
	}
	
	public static AudioOption Time(float time) {
		return Time(time, 0);
	}
	
	public static AudioOption TimeSamples(int timeSamples, float delay) {
		return new AudioOption(OptionTypes.TimeSamples, (float)timeSamples, 0, delay);
	}
	
	public static AudioOption TimeSamples(int timeSamples) {
		return TimeSamples(timeSamples, 0);
	}
	
	public static AudioOption VelocityUpdateMode(AudioVelocityUpdateMode velocityUpdateMode, float delay) {
		return new AudioOption(OptionTypes.VelocityUpdateMode, (float)velocityUpdateMode, 0, delay);
	}
	
	public static AudioOption VelocityUpdateMode(AudioVelocityUpdateMode velocityUpdateMode) {
		return VelocityUpdateMode(velocityUpdateMode, 0);
	}
	
	public static AudioOption IgnoreListenerPause(bool ignoreListenerPause, float delay) {
		return new AudioOption(OptionTypes.IgnoreListenerPause, ignoreListenerPause, 0, delay);
	}
	
	public static AudioOption IgnoreListenerPause(bool ignoreListenerPause) {
		return IgnoreListenerPause(ignoreListenerPause, 0);
	}
	
	public static AudioOption IgnoreListenerVolume(bool ignoreListenerVolume, float delay) {
		return new AudioOption(OptionTypes.IgnoreListenerVolume, ignoreListenerVolume, 0, delay);
	}
	
	public static AudioOption IgnoreListenerVolume(bool ignoreListenerVolume) {
		return IgnoreListenerVolume(ignoreListenerVolume, 0);
	}
	
	public string GetValueDisplayName() {
		return value.GetValueDisplayName();
	}
	
	public T GetValue<T>() {
		return value.GetValue<T>();
	}
	
	public object GetValue() {
		return value.GetValue();
	}

	public void SetValue(object value) {
		this.value.SetValue(value);
	}
	
	public void SetDefaultValue(object value) {
		this.value.SetDefaultValue(value);
	}
	
	public void ResetValue() {
		value.ResetValue();
	}
	
	public void Apply(AudioSetup setup) {
		switch (type) {
			case AudioOption.OptionTypes.Volume:
				float[] volumeData = GetValue<float[]>();
				setup.SetVolume(volumeData[0], volumeData[1], (Ease)volumeData[2], delay);
				break;
			case AudioOption.OptionTypes.Pitch:
				float[] pitchData = GetValue<float[]>();
				setup.SetPitch(pitchData[0], pitchData[1], (Ease)pitchData[2], delay);
				break;
			case AudioOption.OptionTypes.RandomVolume:
				setup.SetOption(setup.SetRandomVolume, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.RandomPitch:
				setup.SetOption(setup.SetRandomPitch, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.FadeIn:
				setup.SetOption(value => setup.FadeIn = value, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.FadeOut:
				setup.SetOption(value => setup.FadeOut = value, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.Clip:
				setup.SetOption(value => setup.Source.clip = value, GetValue<AudioClip>(), delay);
				break;
			case AudioOption.OptionTypes.Output:
				setup.SetOption(value => setup.Source.outputAudioMixerGroup = value, GetValue<AudioMixerGroup>(), delay);
				break;
			case AudioOption.OptionTypes.Loop:
				setup.SetOption(value => setup.Source.loop = value, GetValue<bool>(), delay);
				break;
			case AudioOption.OptionTypes.DopplerLevel:
				setup.SetOption(value => setup.Source.dopplerLevel = value, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.RolloffMode:
				setup.SetOption(value => setup.Source.rolloffMode = value, (AudioRolloffMode)GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.MinDistance:
				setup.SetOption(value => setup.Source.minDistance = value, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.MaxDistance:
				setup.SetOption(value => setup.Source.maxDistance = value, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.Spread:
				setup.SetOption(value => setup.Source.spread = value, GetValue<float>(), delay);
				break;
			case OptionTypes.Mute:
				setup.SetOption(value => setup.Source.mute = value, GetValue<bool>(), delay);
				break;
			case OptionTypes.BypassEffects:
				setup.SetOption(value => setup.Source.bypassEffects = value, GetValue<bool>(), delay);
				break;
			case OptionTypes.BypassListenerEffects:
				setup.SetOption(value => setup.Source.bypassListenerEffects = value, GetValue<bool>(), delay);
				break;
			case OptionTypes.BypassReverbZones:
				setup.SetOption(value => setup.Source.bypassReverbZones = value, GetValue<bool>(), delay);
				break;
			case OptionTypes.Priority:
				setup.SetOption(value => setup.Source.priority = value, (int)GetValue<float>(), delay);
				break;
			case OptionTypes.StereoPan:
				setup.SetOption(value => setup.Source.panStereo = value, GetValue<float>(), delay);
				break;
			case OptionTypes.SpatialBlend:
				setup.SetOption(value => setup.Source.spatialBlend = value, GetValue<float>(), delay);
				break;
			case OptionTypes.ReverbZoneMix:
				setup.SetOption(value => setup.Source.reverbZoneMix = value, GetValue<float>(), delay);
				break;
			case AudioOption.OptionTypes.Time:
				setup.SetOption(value => setup.Source.time = value, GetValue<float>(), delay);
				break;
			case OptionTypes.TimeSamples:
				setup.SetOption(value => setup.Source.timeSamples = value, (int)GetValue<float>(), delay);
				break;
			case OptionTypes.VelocityUpdateMode:
				setup.SetOption(value => setup.Source.velocityUpdateMode = value, (AudioVelocityUpdateMode)GetValue<float>(), delay);
				break;
			case OptionTypes.IgnoreListenerPause:
				setup.SetOption(value => setup.Source.ignoreListenerPause = value, GetValue<bool>(), delay);
				break;
			case OptionTypes.IgnoreListenerVolume:
				setup.SetOption(value => setup.Source.ignoreListenerVolume = value, GetValue<bool>(), delay);
				break;
			default:
				Logger.LogError(string.Format("{0} can not be applied to {1}.", this, setup));
				break;
		}
	}
	
	public override string ToString() {
		return string.Format("AudioOption({0}, {1})", type, GetValueDisplayName());
	}
}
