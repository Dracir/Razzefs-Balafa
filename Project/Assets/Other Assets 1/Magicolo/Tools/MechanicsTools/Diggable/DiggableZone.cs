using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.MechanicsTools {
	[System.Serializable]
	public class DiggableZone {

		public Rect rect;
		public Diggable diggable;
		public GameObject gameObject;
		public bool smallestZone;
		
		DiggableZone parent;
		DiggableZone[] children;
	
		public Vector2 position {
			get {
				return rect.position;
			}
		}
	
		public Vector2 size {
			get {
				return rect.size;
			}
		}
	
		public DiggableZone(Vector2 coordinates, Vector2 size, DiggableZone parent, Diggable diggable) {
			this.rect = new Rect(coordinates.x, coordinates.y, size.x, size.y);
			this.parent = parent;
			this.diggable = diggable;
		
			smallestZone = size.x <= 1;
			
			Initialize();
		}
		
		public void Initialize() {
			diggable.ZoneManager.SetZones(rect, this);
			SetCollider();
		}
	
		public void Update() {
			if (size.x > 64 || size.y > 64) {
				Break();
				return;
			}
		
			for (int y = 0; y < size.y; y++) {
				for (int x = 0; x < size.x; x++) {
					Color pixel = diggable.GetPixel(position.x + x, position.y + y);
				
					if (pixel.a < diggable.AlphaThreshold) {
						Break();
						return;
					}
				}
			}
		}
	
		public void Break() {
			if (smallestZone) {
				if (Application.isPlaying) {
					diggable.SpawnFX(position + size / 2);
				}
			
				diggable.ZoneManager.SetZone(position, null);
				diggable.ZoneManager.DespawnCapsuleCollider(gameObject);
			}
			else {
				children = new [] {
					new DiggableZone(position, size / 2, this, diggable),
					new DiggableZone(position + new Vector2(size.x / 2, 0), size / 2, this, diggable),
					new DiggableZone(position + new Vector2(0, size.y / 2), size / 2, this, diggable),
					new DiggableZone(position + size / 2, size / 2, this, diggable)
				};
		
				foreach (DiggableZone child in children) {
					child.Update();
				}
				
				diggable.ZoneManager.DespawnBoxCollider(gameObject, size.x);
			}
		}
		
		public void Repair() {
			if (parent != null) {
				
			}
		}

		void SetCollider() {
			if (smallestZone) {
				gameObject = diggable.ZoneManager.SpawnCapsuleCollider(rect);
			}
			else {
				gameObject = diggable.ZoneManager.SpawnBoxCollider(rect);
			}
		}
	}
}
