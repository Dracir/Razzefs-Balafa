using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo {
	[System.Serializable]
	public class Pool {

		Queue<GameObject> inactiveObjects;
		Queue<GameObject> InactiveObjects {
			get {
				return inactiveObjects ?? (inactiveObjects = new Queue<GameObject>());
			}
		}
		
		[SerializeField, PropertyField]
		GameObject pooledObject;
		public GameObject PooledObject {
			get {
				return pooledObject;
			}
			set {
				pooledObject = value;
				
				if (pooledObject != null) {
					Transform pooledObjectTransform = pooledObject.transform;
					initialParent = pooledObjectTransform.parent;
					initialPosition = pooledObjectTransform.localPosition;
					initialRotation = pooledObjectTransform.localRotation;
					initialScale = pooledObjectTransform.localScale;
				}
			}
		}

		[SerializeField, Min]
		int maxSize = 100;
		public int MaxSize {
			get {
				return maxSize;
			}
		}
		
		[SerializeField]
		bool sendMessages;
		public bool SendMessages {
			get {
				return sendMessages;
			}
			set {
				sendMessages = value;
			}
		}
		
		int size;
		public int Size {
			get {
				return size;
			}
		}
		
		public bool WillCreate {
			get {
				return InactiveObjects.Count == 0;
			}
		}
		
		public bool WillDestroy {
			get {
				return Size > MaxSize;
			}
		}
		
		[SerializeField, HideInInspector] 
		Transform initialParent;
		[SerializeField, HideInInspector]
		Vector3 initialPosition;
		[SerializeField, HideInInspector] 
		Quaternion initialRotation;
		[SerializeField, HideInInspector] 
		Vector3 initialScale;

		public Pool(GameObject pooledObject) {
			PooledObject = pooledObject;
		}
		
		public Pool(GameObject pooledObject, int maxSize) {
			PooledObject = pooledObject;
			this.maxSize = maxSize;
		}
		
		public Pool(Component pooledObject) {
			PooledObject = pooledObject.gameObject;
		}
		
		public Pool(Component pooledObject, int maxSize) {
			PooledObject = pooledObject.gameObject;
			this.maxSize = maxSize;
		}
		
		Pool() {
			maxSize = 100;
		}
		
		public GameObject Spawn(Transform parent, Vector3 position, Quaternion rotation, Vector3 scale) {
			if (pooledObject == null) {
				return null;
			}
			
			GameObject spawn = null;
			
			while (spawn == null) {
				if (InactiveObjects.Count > 0) {
					spawn = InactiveObjects.Dequeue();
				}
				else {
					spawn = Create();
				}
			}
			
			spawn.SetActive(true);
			
			Transform spawnTransform = spawn.transform;
			spawnTransform.parent = parent;
			spawnTransform.localPosition = position;
			spawnTransform.localRotation = rotation;
			spawnTransform.localScale = scale;
			
			if (SendMessages) {
				spawn.SendMessage("OnSpawned", SendMessageOptions.DontRequireReceiver);
			}
			
			return spawn;
		}

		public GameObject Spawn(Transform parent, Vector3 position, Quaternion rotation) {
			return Spawn(parent, position, rotation, initialScale);
		}
		
		public GameObject Spawn(Transform parent, Vector3 position) {
			return Spawn(parent, position, initialRotation, initialScale);
		}

		public GameObject Spawn(Transform parent) {
			return Spawn(parent, initialPosition, initialRotation, initialScale);
		}

		public GameObject Spawn() {
			return Spawn(initialParent, initialPosition, initialRotation, initialScale);
		}
		
		public T Spawn<T>(Transform parent, Vector3 position, Quaternion rotation, Vector3 scale) where T : Component {
			return Spawn(parent, position, rotation, scale).GetComponent<T>();
		}

		public T Spawn<T>(Transform parent, Vector3 position, Quaternion rotation) where T : Component {
			return Spawn<T>(parent, position, rotation, initialPosition);
		}

		public T Spawn<T>(Transform parent, Vector3 position) where T : Component {
			return Spawn<T>(parent, position, initialRotation, initialScale);
		}
		
		public T Spawn<T>(Transform parent) where T : Component {
			return Spawn<T>(parent, initialPosition, initialRotation, initialScale);
		}

		public T Spawn<T>() where T : Component {
			return Spawn<T>(initialParent, initialPosition, initialRotation, initialScale);
		}
		
		public void Despawn(GameObject spawn) {
			if (SendMessages) {
				spawn.SendMessage("OnDespawned", SendMessageOptions.DontRequireReceiver);
			}
			
			if (MaxSize == 0 || Size <= MaxSize) {
				spawn.SetActive(false);
				InactiveObjects.Enqueue(spawn);
			}
			else {
				Destroy(spawn);
			}
		}
		
		public void Despawn(Component spawn) {
			Despawn(spawn.gameObject);
		}
		
		GameObject Create() {
			size += 1;
			return Object.Instantiate(pooledObject);
		}
		
		void Destroy(GameObject spawn) {
			spawn.Remove();
			size -= 1;
		}
	}
}