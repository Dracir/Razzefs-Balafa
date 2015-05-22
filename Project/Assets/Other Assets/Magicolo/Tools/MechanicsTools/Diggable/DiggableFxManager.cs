using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.MechanicsTools {
	public class DiggableFxManager : MonoBehaviourExtended {

		public Diggable diggable;
		
		bool initialized;
		ParticleSystem parentFX;
		ParticleSystem[] FXs;
		
		public void Initialize(Diggable diggable) {
			this.diggable = diggable;
			
			if (diggable.DigFX != null) {
				parentFX = Instantiate(diggable.DigFX).GetComponent<ParticleSystem>();
				parentFX.transform.parent = transform;
				parentFX.enableEmission = false;
				FXs = parentFX.GetComponentsInChildren<ParticleSystem>();
				initialized = true;
			}
		}
		
		public void SpawnFX(Vector2 pixel, float strength) {
			if (!initialized) {
				return;
			}
			
			parentFX.transform.localPosition = pixel;
			
			foreach (ParticleSystem fx in FXs) {
				fx.Emit((int)Mathf.Max(fx.emissionRate * strength, 1));
			}
		}
	}
}