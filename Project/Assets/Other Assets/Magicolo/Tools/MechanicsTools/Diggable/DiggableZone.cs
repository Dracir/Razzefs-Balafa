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
		}
		
		public void Initialize() {
			diggable.ZoneManager.SetZones(rect, this);
			SetCollider();
		}
	
		public void Update(int x, int y) {
			if (size.x > 64 || size.y > 64) {
				Break(-1, -1);
				return;
			}
		
			if (rect.Contains(new Vector2(x, y))) {
				Break(x, y);
				return;
			}
			
			Initialize();
		}
	
		public void Update() {
			if (size.x > 64 || size.y > 64) {
				Break(-1, -1);
				return;
			}
		
			for (int y = (int)position.y; y < size.y; y++) {
				for (int x = (int)position.x; x < size.x; x++) {
					Color pixel = diggable.GetPixel(x, y);
				
					if (pixel.a < diggable.AlphaThreshold) {
						Break(x, y);
						return;
					}
				}
			}
			
			Initialize();
		}
	
		public void Break(int x, int y) {
			if (smallestZone) {
				diggable.ZoneManager.SetZone(position, null);
				diggable.ColliderManager.DespawnCapsuleCollider(gameObject);
			}
			else {
				children = new [] {
					new DiggableZone(position, size / 2, this, diggable),
					new DiggableZone(position + new Vector2(size.x / 2, 0), size / 2, this, diggable),
					new DiggableZone(position + new Vector2(0, size.y / 2), size / 2, this, diggable),
					new DiggableZone(position + size / 2, size / 2, this, diggable)
				};
		
				foreach (DiggableZone child in children) {
					if (x == -1 || y == -1) {
						child.Update();
					}
					else {
						child.Update(x, y);
					}
				}
				
				diggable.ColliderManager.DespawnBoxCollider(gameObject, size.x);
			}
		}
		
		public void Repair() {
			if (parent != null) {
				
			}
		}

		void SetCollider() {
			if (smallestZone) {
				gameObject = diggable.ColliderManager.SpawnCapsuleCollider(rect);
			}
			else {
				gameObject = diggable.ColliderManager.SpawnBoxCollider(rect);
			}
		}
	}
}
