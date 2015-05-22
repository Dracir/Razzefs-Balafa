using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

public enum Music {
	Level1
}
	
public class MusicPlayer : MonoBehaviourExtended {

	static MusicPlayer instance;
	static MusicPlayer Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<MusicPlayer>();
			}
			
			return instance;
		}
	}
	
	bool _audioPlayerCached;
	AudioPlayer _audioPlayer;
	public AudioPlayer audioPlayer { 
		get { 
			_audioPlayer = _audioPlayerCached ? _audioPlayer : this.FindComponent<AudioPlayer>();
			_audioPlayerCached = true;
			return _audioPlayer;
		}
	}
    
	Dictionary<Music, AudioSourceItem> musicItemDict;
	Dictionary<Music, AudioSourceItem> MusicItemDict {
		get {
			if (musicItemDict == null) {
				musicItemDict = new Dictionary<Music, AudioSourceItem>();
			}
			
			return musicItemDict;
		}
	}
	
	void Awake() {
		if (Instance != null && Instance != this) {
			gameObject.Remove();
			return;
		}
		
		DontDestroyOnLoad(gameObject);
		instance = this;
	}
	
	public static void Play(Music music) {
		Instance.MusicItemDict[music] = Instance.audioPlayer.Play(music.ToString());
	}
	
	public static bool IsPlaying(Music music) {
		return Instance.MusicItemDict.ContainsKey(music) && Instance.MusicItemDict[music].IsPlaying;
	}
}

