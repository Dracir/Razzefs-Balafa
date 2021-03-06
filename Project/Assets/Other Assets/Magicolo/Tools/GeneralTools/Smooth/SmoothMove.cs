﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo {
	[AddComponentMenu("Magicolo/Smooth/Move")]
	public class SmoothMove : MonoBehaviourExtended {

		[Mask] public TransformModes mode = TransformModes.Position;
		[Mask(Axes.XYZ)] public Axes axis = Axes.XYZ;
		public bool culling = true;
	
		[Slider(BeforeSeparator = true)] public float randomness;
		public Vector3 speed = Vector3.one;
	
		bool _rendererCached;
		Renderer _renderer;
		new public Renderer renderer { 
			get { 
				_renderer = _rendererCached ? _renderer : GetComponent<Renderer>();
				_rendererCached = true;
				return _renderer;
			}
		}
		
		void Awake() {
			ApplyRandomness();
		}
		
		void Update() {
			if (mode == TransformModes.None || axis == Axes.None) {
				return;
			}
			
			if (!culling || renderer.isVisible) {
				if (mode.Contains(TransformModes.Position)) {
					transform.TranslateLocal(speed, axis);
				}
				
				if (mode.Contains(TransformModes.Rotation)) {
					transform.RotateLocal(speed, axis);
					
				}
				
				if (mode.Contains(TransformModes.Scale)) {
					transform.ScaleLocal(speed, axis);
					
				}
			}
		}
		
		public void ApplyRandomness() {
			speed += speed.SetValues(new Vector3(Random.Range(-randomness * speed.x, randomness * speed.x), Random.Range(-randomness * speed.y, randomness * speed.y), Random.Range(-randomness * speed.z, randomness * speed.z)), axis);
		}
	}
}