using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.MechanicsTools {
	public class DiggableColliderManager : MonoBehaviourExtended {

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
		
		const float margin = 0.01F;
		
		public void Initialize(Diggable diggable) {
			this.diggable = diggable;
		}
		
		public GameObject SpawnBoxCollider(Rect rect) {
			return SpawnBoxCollider(rect.position, rect.size);
		}
	
		public GameObject SpawnBoxCollider(Vector2 pixel, Vector2 size) {
			return GetBoxColliderPool(size.x).Spawn(transform, pixel);
		}
		
		public void DespawnBoxCollider(GameObject boxCollider, float size) {
			if (boxCollider != null) {
				GetBoxColliderPool(size).Despawn(boxCollider);
			}
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
			if (capsuleCollider != null) {
				CapsuleColliderPool.Despawn(capsuleCollider);
			}
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
			colliderTransform.SetLocalScale(size, Axes.XY);
			colliderTransform.SetScale(1, Axes.Z);
			
			return colliderObject.AddComponent<T>();
		}
	}
}