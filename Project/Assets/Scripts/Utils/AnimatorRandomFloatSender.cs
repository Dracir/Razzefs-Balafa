using UnityEngine;
using System.Collections;

public class AnimatorRandomFloatSender : MonoBehaviour {

	Animator animator;
	public string[] animationString = new string[]{"Random"};
	
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	
	void Update () {
		foreach (var str in animationString) {
			animator.SetFloat(str, Random.Range(0f,1f));
		}
	}
}
