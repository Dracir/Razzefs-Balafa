using UnityEngine;
using System.Collections;
using RickTools;
using Rick.RandomBags;

public class GeneralSoundPlayer : MonoBehaviour {


	public AudioClip[] onStart;
	public AudioClip[] oneTime;
	public AudioClip[] loop;

	public bool loopAfterOneTime;
	public bool loopAfterOnStart;
	public bool random;
	
	int index;

	RoundRobinBag<int> bags = new RoundRobinBag<int>(RoundRobinBagOptions.SUFFLE_ON_END, RoundRobinBagOptions.SUFFLE_ON_ADD);

	AudioSource _source;
	AudioSource Source {
		get{
			if (_source == null || _source.isPlaying){
				_source = gameObject.AddComponent<AudioSource>();
			}
			return _source;
		}
	}
	// Use this for initialization
	void Start () {
		if (random){
			for (int i = 0; i < oneTime.Length; i ++){
				bags.add(i);
			}
		}
		
		if (onStart.Length > 0){
			int i = 0;
			if (random && onStart.Length > 1){
				i = Random.Range(0, onStart.Length);
			}
			Source.PlayOneShot(onStart[i]);

			if (loopAfterOnStart){
				if (random){
					i = Random.Range (0, loop.Length);
				}
				AudioSource loopSource = NewSource();
				loopSource.clip = loop[i];
				loopSource.loop = true;
				loopSource.PlayDelayed(onStart[i].length);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		//TEST
		if (Input.GetKeyDown(KeyCode.Space)){
			OneShot();
		}
	}

	AudioSource NewSource () {
		return gameObject.AddComponent<AudioSource>();
	}

	void OneShot () {
		OneShot(loopAfterOneTime);
	}

	void OneShot (bool loopAfter) {
		int i = index;
		if (random){
			i = bags.next();
		}
		Source.loop = false;
		Source.clip = oneTime[i];
		Source.Play();
		if (loopAfter){
			Source.clip = loop[random? Random.Range (0, loop.Length) : index % oneTime.Length];
			Source.loop = true;
			Source.PlayDelayed(oneTime[i].length);
			
			//			AudioSource loopSource = NewSource();
			//			loopSource.clip = loop[random? Random.Range (0, loop.Length) : index % oneTime.Length];
			//			loopSource.loop = true;
			//			loopSource.PlayDelayed(oneTime[i].length);
		}
		index ++;
	}

	void Loop () {
		int i = index;
		if (random){
			i = Random.Range(0, loop.Length);
		}
		Source.clip = loop[i];
		Source.loop = true;
		Source.Play ();
	}
}
