using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using Magicolo;
using DG.Tweening;

namespace Magicolo.AudioTools {
	[RequireComponent(typeof(AudioSource))]
	public class AudioSetup : MonoBehaviourExtended {

		AudioStates state;
		public AudioStates State {
			get {
				return state;
			}
		}
		
		bool _sourceCached;
		AudioSource _source;
		public AudioSource Source { 
			get { 
				_source = _sourceCached ? _source : GetComponent<AudioSource>();
				_sourceCached = true;
				return _source;
			}
		}
		
		public AudioClip Clip {
			get {
				return Source.clip;
			}
		}
		
		[SerializeField, PropertyField]
		float fadeIn;
		public float FadeIn {
			get {
				return fadeIn;
			}
			set {
				fadeIn = value;
				fadeOut = Mathf.Clamp(fadeOut, 0, Length - fadeIn);
				fadeIn = Mathf.Clamp(fadeIn, 0, Length);
				fadeOut = Mathf.Clamp(fadeOut, 0, Length);
			}
		}
		
		[SerializeField, PropertyField]
		Ease fadeInEase = Ease.OutQuad;
		public Ease FadeInEase {
			get {
				return fadeInEase;
			}
			set {
				fadeInEase = value;
			}
		}
		
		[SerializeField, PropertyField]
		float fadeOut;
		public float FadeOut {
			get {
				return fadeOut;
			}
			set {
				fadeOut = value;
				fadeIn = Mathf.Clamp(fadeIn, 0, Length - fadeOut);
				fadeIn = Mathf.Clamp(fadeIn, 0, Length);
				fadeOut = Mathf.Clamp(fadeOut, 0, Length);
			}
		}
		
		[SerializeField, PropertyField]
		Ease fadeOutEase = Ease.InQuad;
		public Ease FadeOutEase {
			get {
				return fadeOutEase;
			}
			set {
				fadeOutEase = value;
			}
		}
		
		[SerializeField, PropertyField(typeof(RangeAttribute), 0, 1)]
		float randomVolume;
		public float RandomVolume {
			get {
				return randomVolume;
			}
			set {
				randomVolume = Mathf.Clamp01(value);
			}
		}
		
		[SerializeField, PropertyField(typeof(RangeAttribute), 0, 1)]
		float randomPitch;
		public float RandomPitch {
			get {
				return randomPitch;
			}
			set {
				randomPitch = Mathf.Clamp01(value);
				ApplyRandomPitch();
			}
		}

		[SerializeField, PropertyField]
		AudioPlayer audioPlayer;
		public AudioPlayer AudioPlayer {
			get {
				return audioPlayer;
			}
		}
		
		float Length {
			get {
				return Clip.length / Source.pitch;
			}
		}
		
		Tweener volumeTweener;
		Tweener pitchTweener;
		IEnumerator stopRoutine;
		float initialVolume;
		float initialPitch;
		
		AudioItemCallback onPlayCallback;
		AudioItemCallback onStopCallback;
		AudioItemCallback onPauseCallback;
		AudioItemCallback onUnPauseCallback;
		
		void Awake() {
			volumeTweener = Source.DOFade(Source.volume, 0);
			pitchTweener = DOTween.To(() => Source.pitch, value => Source.pitch = value, Source.pitch, 0);
			stopRoutine = StopAfterDelay(0);
			
			initialVolume = Source.volume;
			initialPitch = Source.pitch;
		}

		void OnDestroy() {
			if (AudioPlayer != null) {
				AudioPlayer.SetupManager.RemoveSetup(this);
			}
		}
		
		public void Initialize(AudioPlayer audioPlayer) {
			this.audioPlayer = audioPlayer;
		}
		
		public void Play() {
			volumeTweener.Kill();
			StopCoroutine(stopRoutine);
			
			InitializeSource();
			
			if (FadeIn > 0) {
				float targetVolume = Source.volume;
			
				Source.volume = 0;
				volumeTweener = Source.DOFade(targetVolume, FadeIn).SetEase(fadeInEase).OnComplete(FadeInComplete);
				
				state = AudioStates.FadingIn;
			}
			else {
				state = AudioStates.Playing;
			}

			Source.Play();
			
			if (!Source.loop) {
				stopRoutine = StopAfterDelay(Length - FadeOut);
				StartCoroutine(stopRoutine);
			}
			
			if (onPlayCallback != null) {
				onPlayCallback();
			}
		}
		
		public void Play(float delay) {
			if (delay > 0) {
				StartCoroutine(DelayedAction(Play, delay));
			}
			else {
				Play();
			}
		}
		
		public void Pause() {
			if (State != AudioStates.Paused && State != AudioStates.Stopped) {
				volumeTweener.Pause();
				pitchTweener.Pause();
			
				Source.Pause();
				
				state = AudioStates.Paused;
				
				if (onPauseCallback != null) {
					onPauseCallback();
				}
			}
		}
		
		public void Pause(float delay) {
			if (delay > 0) {
				StartCoroutine(DelayedAction(Pause, delay));
			}
			else {
				Pause();
			}
		}
		
		public void UnPause() {
			if (state == AudioStates.Paused) {
				volumeTweener.Play();
				pitchTweener.Play();
			
				Source.UnPause();
				
				state = AudioStates.Playing;
				
				if (onUnPauseCallback != null) {
					onUnPauseCallback();
				}
			}
		}
		
		public void UnPause(float delay) {
			if (delay > 0) {
				StartCoroutine(DelayedAction(UnPause, delay));
			}
			else {
				UnPause();
			}
		}
		
		public void Stop() {
			if (State == AudioStates.FadingIn || State == AudioStates.Playing) {
				volumeTweener.Kill();
				
				if (FadeOut > 0) {
					volumeTweener = Source.DOFade(0, FadeOut).SetEase(fadeOutEase).OnComplete(FadeOutComplete);
					
					state = AudioStates.FadingOut;
				}
				else {
					StopImmediate();
				}
			}
			else if (State == AudioStates.Paused) {
				StopImmediate();
			}
		}
		
		public void Stop(float delay) {
			if (delay > 0) {
				StartCoroutine(DelayedAction(Stop, delay));
			}
			else {
				Stop();
			}
		}
		
		public void StopImmediate() {
			if (State != AudioStates.Stopped) {
				volumeTweener.Kill();
			
				Source.Stop();
			
				state = AudioStates.Stopped;
				
				if (onStopCallback != null) {
					onStopCallback();
				}
			}
		}
		
		public void StopImmediate(float delay) {
			if (delay > 0) {
				StartCoroutine(DelayedAction(StopImmediate, delay));
			}
			else {
				StopImmediate();
			}
		}

		public void SetVolume(float volume, float time, Ease ease, float delay) {
			volumeTweener.Kill();
			volumeTweener = Source.DOFade(volume, time).SetEase(ease).SetDelay(delay);
		}
		
		public void SetPitch(float pitch, float time, Ease ease, float delay) {
			pitchTweener.Kill();
			pitchTweener = DOTween.To(() => Source.pitch, value => Source.pitch = value, pitch, time).SetEase(ease).SetDelay(delay);
		}

		public void SetOption<T>(OptionSetter<T> setter, T value, float delay) {
			if (delay > 0) {
				StartCoroutine(DelayedSet(setter, value, delay));
			}
			else {
				setter(value);
			}
		}
		
		public void SetRandomVolume(float randomVolume) {
			RandomVolume = randomVolume;
			ApplyRandomVolume();
		}
		
		public void SetRandomPitch(float randomPitch) {
			RandomPitch = randomPitch;
			ApplyRandomPitch();
		}
		
		public void ApplyRandomVolume() {
			Source.volume = initialVolume + initialVolume * Random.Range(-RandomVolume, RandomVolume);
		}
		
		public void ApplyRandomPitch() {
			Source.pitch = initialPitch + initialPitch * Random.Range(-RandomPitch, RandomPitch);
		}
		
		public void OnPlay(AudioItemCallback onPlayCallback) {
			this.onPlayCallback = onPlayCallback;
		}
		
		public void OnStop(AudioItemCallback onStopCallback) {
			this.onStopCallback = onStopCallback;
		}
		
		public void OnPause(AudioItemCallback onPauseCallback) {
			this.onPauseCallback = onPauseCallback;
		}
		
		public void OnUnPause(AudioItemCallback onUnPauseCallback) {
			this.onUnPauseCallback = onUnPauseCallback;
		}
		
		void InitializeSource() {
			ApplyRandomVolume();
			ApplyRandomPitch();
		}
		
		void FadeInComplete() {
			state = AudioStates.Playing;
		}
		
		void FadeOutComplete() {
			StopImmediate();
		}
		
		IEnumerator DelayedAction(System.Action action, float delay) {
			float counter = delay;
			
			while (counter > Time.deltaTime) {
				yield return new WaitForSeconds(0);
				
				counter -= Time.deltaTime;
			}
			
			action();
		}
		
		IEnumerator DelayedSet<T>(OptionSetter<T> setter, T value, float delay) {
			float counter = delay;
			
			while (counter > Time.deltaTime) {
				yield return new WaitForSeconds(0);
				
				counter -= Time.deltaTime;
			}
			
			setter(value);
		}
		
		IEnumerator StopAfterDelay(float delay) {
			float counter = delay;
			
			while (counter > Time.deltaTime && State != AudioStates.FadingOut && State != AudioStates.Stopped) {
				yield return new WaitForSeconds(0);
				
				if (State != AudioStates.Paused) {
					counter -= Time.deltaTime;
				}
			}
			
			Stop();
		}
	}
}