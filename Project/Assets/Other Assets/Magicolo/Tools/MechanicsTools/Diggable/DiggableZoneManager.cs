using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.MechanicsTools {
	public class DiggableZoneManager : MonoBehaviourExtended {

		[HideInInspector] public DiggableZone[] zones;
		public Diggable diggable;
	
		Dictionary<float, Pool> boxColliderPools;
		Dictionary<float, Pool> BoxColliderPools {
			get {
				return boxColliderPools ?? (boxColliderPools = new Dictionary<float, Pool>());
			}
		}
		
		Pool capsuleColliderPool;
		Pool CapsuleColliderPool {
			get {
				if (capsuleColliderPool == null) {
					CreateCapsuleColliderPool();
				}
				
				return capsuleColliderPool;
			}
		}
		
		const float margin = 0.001F;
		
		public void Initialize(Diggable diggable) {
			this.diggable = diggable;
		
			zones = new DiggableZone[diggable.Width * diggable.Height];
			DiggableZone originalZone = new DiggableZone(Vector2.zero, new Vector2(diggable.Width, diggable.Height), null, diggable);
			
			originalZone.Update();
		}
	
		public DiggableZone GetZone(Vector2 coordinates) {
			return GetZone((int)coordinates.x, (int)coordinates.y);
		}
	
		public DiggableZone GetZone(float x, float y) {
			return GetZone((int)x, (int)y);
		}
	
		public DiggableZone GetZone(int x, int y) {
			DiggableZone zone = null;
		
			try {
				zone = zones[y * diggable.Width + x];
			}
			catch {
				Logger.LogError(string.Format("Zone at coordinates [{0}, {1}] was not found.", x, y));
			}
		
			return zone;
		}
	
		public void SetZone(int x, int y, DiggableZone zone) {
			zones[y * diggable.Width + x] = zone;
		}
	
		public void SetZone(Vector2 coordinates, DiggableZone zone) {
			SetZone((int)coordinates.x, (int)coordinates.y, zone);
		}
	
		public void SetZones(Rect rect, DiggableZone zone) {
			SetZones(rect.position, rect.size, zone);
		}
	
		public void SetZones(Vector2 coordinates, Vector2 size, DiggableZone zone) {
			for (int y = 0; y < size.y; y++) {
				for (int x = 0; x < size.x; x++) {
					SetZone(new Vector2(coordinates.x + x, coordinates.y + y), zone);
				}
			}
		}
	
		public GameObject SpawnBoxCollider(Rect rect) {
			return SpawnBoxCollider(rect.position, rect.size);
		}
	
		public GameObject SpawnBoxCollider(Vector2 pixel, Vector2 size) {
			return GetBoxColliderPool(size.x).Spawn(transform, pixel);
		}
		
		public void DespawnBoxCollider(GameObject boxCollider, float size) {
			GetBoxColliderPool(size).Despawn(boxCollider);
		}
		
		Pool GetBoxColliderPool(float size) {
			Pool colliderPool;
		
			if (BoxColliderPools.ContainsKey(size)) {
				colliderPool = BoxColliderPools[size];
			}
			else {
				BoxCollider boxCollider = CreateCollider<BoxCollider>(size);
				boxCollider.center = new Vector2(0.5F, 0.5F);
				boxCollider.size = new Vector3(1 - margin, 1 - margin, 1);
		
				colliderPool = new Pool(boxCollider);
				BoxColliderPools[size] = colliderPool;
			}
		
			return colliderPool;
		}
	
		public GameObject SpawnCapsuleCollider(Rect rect) {
			return SpawnCapsuleCollider(rect.position);
		}
		
		public GameObject SpawnCapsuleCollider(Vector2 pixel) {
			return CapsuleColliderPool.Spawn(transform, pixel);
		}
		
		public void DespawnCapsuleCollider(GameObject capsuleCollider) {
			CapsuleColliderPool.Despawn(capsuleCollider);
		}
		
		void CreateCapsuleColliderPool() {
			CapsuleCollider capsuleCollider = CreateCollider<CapsuleCollider>(1);
			capsuleCollider.center = new Vector2(0.5F, 0.5F);
			capsuleCollider.radius = 0.5F - margin;
			capsuleCollider.direction = 2;
		
			capsuleColliderPool = new Pool(capsuleCollider);
		}
		
		T CreateCollider<T>(float size) where T : Collider {
			GameObject colliderObject = this.AddChild("Collider");
			colliderObject.layer = diggable.Layer;
			colliderObject.SetActive(false);
		
			Transform colliderTransform = colliderObject.transform;
			colliderTransform.SetLocalScale(size, Axis.XY);
			colliderTransform.SetScale(1, Axis.Z);
			
			return colliderObject.AddComponent<T>();
		}
	}
}