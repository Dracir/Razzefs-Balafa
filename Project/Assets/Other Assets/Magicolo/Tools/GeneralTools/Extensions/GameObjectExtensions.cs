using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Magicolo {
	public static class GameObjectExtensions {

		public static GameObject[] GetParents(this GameObject child) {
			Transform[] parentsTransform = child.transform.GetParents();
			GameObject[] parents = new GameObject[parentsTransform.Length];
			
			for (int i = 0; i < parentsTransform.Length; i++) {
				parents[i] = parentsTransform[i].gameObject;
			}
			
			return parents;
		}
		
		public static GameObject[] GetChildren(this GameObject parent) {
			Transform[] childrenTransform = parent.transform.GetChildren();
			GameObject[] children = new GameObject[childrenTransform.Length];
			
			for (int i = 0; i < childrenTransform.Length; i++) {
				children[i] = childrenTransform[i].gameObject;
			}
			
			return children;
		}
	
		public static GameObject[] GetChildrenRecursive(this GameObject parent) {
			Transform[] childrenTransform = parent.transform.GetChildrenRecursive();
			GameObject[] children = new GameObject[childrenTransform.Length];
			
			for (int i = 0; i < childrenTransform.Length; i++) {
				children[i] = childrenTransform[i].gameObject;
			}
			
			return children;
		}
		
		public static int GetChildCount(this GameObject parent) {
			return parent.transform.childCount;
		}
	
		public static GameObject GetChild(this GameObject parent, int index) {
			return parent.transform.GetChild(index).gameObject;
		}
	
		public static GameObject FindChild(this GameObject parent, string childName) {
			foreach (Transform child in parent.transform.GetChildren()) {
				if (child.name == childName) {
					return child.gameObject;
				}
			}
			
			return null;
		}

		public static GameObject FindChildRecursive(this GameObject parent, string childName) {
			foreach (Transform child in parent.transform.GetChildrenRecursive()) {
				if (child.name == childName) {
					return child.gameObject;
				}
			}
			
			return null;
		}
	
		public static GameObject AddChild(this GameObject parent, string childName, PrimitiveType primitiveType) {
			return parent.transform.AddChild(childName, primitiveType).gameObject;
		}
			
		public static GameObject AddChild(this GameObject parent, string childName) {
			return parent.transform.AddChild(childName).gameObject;
		}
		
		public static GameObject FindOrAddChild(this GameObject parent, string childName, PrimitiveType primitiveType) {
			return parent.transform.FindOrAddChild(childName, primitiveType).gameObject;
		}
	
		public static GameObject FindOrAddChild(this GameObject parent, string childName) {
			return parent.transform.FindOrAddChild(childName).gameObject;
		}
	
		public static void SortChildren(this GameObject parent) {
			parent.transform.SortChildren();
		}
	
		public static void SortChildrenRecursive(this GameObject parent) {
			parent.transform.SortChildrenRecursive();
		}
	
		public static int GetHierarchyDepth(this GameObject gameObject) {
			return gameObject.transform.GetHierarchyDepth();
		}
	
		public static T GetComponentInChildrenExclusive<T>(this GameObject gameObject) where T : Component {
			return (T)gameObject.GetComponentInChildrenExclusive(typeof(T));
		}
		
		public static Component GetComponentInChildrenExclusive(this GameObject gameObject, Type componentType) {
			Component component = null;
			
			foreach (GameObject child in gameObject.GetChildren()) {
				component = child.GetComponentInChildren(componentType);
				
				if (component != null) {
					break;
				}
			}
			
			return component;
		}
		
		public static T[] GetComponentsInChildrenExclusive<T>(this GameObject gameObject) where T : Component {
			List<T> components = new List<T>();
			
			foreach (GameObject child in gameObject.GetChildren()) {
				components.AddRange(child.GetComponentsInChildren<T>());
			}
			
			return components.ToArray();
		}
		
		public static Component[] GetComponentsInChildrenExclusive(this GameObject gameObject, Type componentType) {
			List<Component> components = new List<Component>();
			
			foreach (GameObject child in gameObject.GetChildren()) {
				components.AddRange(child.GetComponentsInChildren(componentType));
			}
			
			return components.ToArray();
		}
		
		public static T GetComponentInParentExclusive<T>(this GameObject gameObject) where T : Component {
			return (T)gameObject.GetComponentInParentExclusive(typeof(T));
		}
		
		public static Component GetComponentInParentExclusive(this GameObject gameObject, Type componentType) {
			Transform parent = gameObject.transform.parent;
			return parent != null ? parent.GetComponentInParent(componentType) : null;
		}
		
		public static T[] GetComponentsInParentExclusive<T>(this GameObject gameObject) where T : Component {
			Transform parent = gameObject.transform.parent;
			return parent != null ? parent.GetComponentsInParent<T>() : new T[0];
		}
		
		public static Component[] GetComponentsInParentExclusive(this GameObject gameObject, Type componentType) {
			Transform parent = gameObject.transform.parent;
			return parent != null ? parent.GetComponentsInParent(componentType) : new Component[0];
		}
		
		public static T FindComponent<T>(this GameObject gameObject) where T : Component {
			return (T)gameObject.FindComponent(typeof(T));
		}
		
		public static Component FindComponent(this GameObject gameObject, Type componentType) {
			Component component = gameObject.GetComponent(componentType);
		
			if (component == null) {
				component = gameObject.GetComponentInParentExclusive(componentType);
			}
			
			if (component == null) {
				component = gameObject.GetComponentInChildrenExclusive(componentType);
			}
			
			return component;
		}
		
		public static T[] FindComponents<T>(this GameObject gameObject) where T : Component {
			T[] selfComponents = gameObject.GetComponents<T>();
			T[] parentsComponents = gameObject.GetComponentsInParentExclusive<T>();
			T[] childrenComponents = gameObject.GetComponentsInChildrenExclusive<T>();
			
			T[] components = new T[selfComponents.Length + parentsComponents.Length + childrenComponents.Length];
			selfComponents.CopyTo(components, 0);
			parentsComponents.CopyTo(components, selfComponents.Length);
			childrenComponents.CopyTo(components, selfComponents.Length + parentsComponents.Length);

			return components;
		}
		
		public static Component[] FindComponents(this GameObject gameObject, Type componentType) {
			Component[] selfComponents = gameObject.GetComponents(componentType);
			Component[] parentsComponents = gameObject.GetComponentsInParentExclusive(componentType);
			Component[] childrenComponents = gameObject.GetComponentsInChildrenExclusive(componentType);
			
			Component[] components = new Component[selfComponents.Length + parentsComponents.Length + childrenComponents.Length];
			selfComponents.CopyTo(components, 0);
			parentsComponents.CopyTo(components, selfComponents.Length);
			childrenComponents.CopyTo(components, selfComponents.Length + parentsComponents.Length);

			return components;
		}
		
		public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component {
			return (T)gameObject.GetOrAddComponent(typeof(T));
		}
	
		public static Component GetOrAddComponent(this GameObject gameObject, Type componentType) {
			Component component = gameObject.GetComponent(componentType);
			
			if (component == null) {
				component = gameObject.AddComponent(componentType);
			}
			
			return component;
		}
	
		public static T AddCopiedComponent<T>(this GameObject copyTo, T copyFrom) where T : Component {
			T component = copyTo.AddComponent<T>();
			component.Copy((T)copyFrom);
			
			return component;
		}

		public static Component[] AddCopiedComponents(this GameObject copyTo, params Component[] copyFrom) {
			List<Component> components = new List<Component>();
			
			foreach (Component component in copyFrom) {
				components.Add(copyTo.AddCopiedComponent(component));
			}
			
			return components.ToArray();
		}
	
		public static Component[] AddCopiedComponents(this GameObject copyTo, GameObject copyFrom, params Type[] typesToIgnore) {
			List<Component> clonedComponents = new List<Component>();
			Component[] dstComponents = copyFrom.GetComponents(typeof(Component));
		
			foreach (Component dstComponent in dstComponents) {
				if (!typesToIgnore.Contains(dstComponent.GetType())) {
					if (dstComponent is Transform || (dstComponent is ParticleSystemRenderer && dstComponents.Contains(typeof(ParticleSystem)))) {
						copyTo.CopyComponent(dstComponent);
					}
					else {
						Component clonedComponent = copyTo.AddCopiedComponent(dstComponent);
						
						if (clonedComponent != null) {
							clonedComponents.Add(clonedComponent);
						}
					}
				}
			}
			
			return clonedComponents.ToArray();
		}
	
		public static Component CopyComponent(this GameObject copyTo, Component copyFrom) {
			Component clonedComponent = copyTo.GetComponent(copyFrom.GetType());
			
			if (clonedComponent == null) {
				Logger.LogError("Component of type " + copyFrom.GetType() + " was not found on the GameObject.");
			}
			else {
				clonedComponent.Copy(copyFrom);
			}
			
			return clonedComponent;
		}
	
		public static Component[] CopyComponents(this GameObject copyTo, params Component[] copyFrom) {
			List<Component> clonedComponents = new List<Component>();
		
			foreach (Component dstComponent in copyFrom) {
				Component clonedComponent = copyTo.CopyComponent(dstComponent);
				if (clonedComponent != null) {
					clonedComponents.Add(clonedComponent);
				}
			}
			
			return clonedComponents.ToArray();
		}
	
		public static Component[] CopyComponents(this GameObject copyTo, GameObject copyFrom, params Type[] typesToIgnore) {
			List<Component> clonedComponents = new List<Component>();
			Component[] dstComponents = copyFrom.GetComponents(typeof(Component));
		
			foreach (Component dstComponent in dstComponents) {
				if (!typesToIgnore.Contains(dstComponent.GetType())) {
					Component clonedComponent = copyTo.CopyComponent(dstComponent);
					
					if (clonedComponent != null) {
						clonedComponents.Add(clonedComponent);
					}
				}
			}
			
			return clonedComponents.ToArray();
		}
		
		public static void RemoveComponent<T>(this GameObject gameObject) where T : Component {
			T toRemove = gameObject.GetComponent<T>();
			
			if (toRemove != null) {
				toRemove.Remove();
			}
		}
	
		public static T GetClosest<T>(this GameObject source, IList<T> targets) where T : Component {
			float closestDistance = float.MaxValue;
			T closestTarget = default(T);

			foreach (T target in targets) {
				float distance = Vector3.Distance(source.transform.position, target.transform.position);
				
				if (distance < closestDistance) {
					closestTarget = target;
					closestDistance = distance;
				}
			}
			
			return closestTarget;
		}

		public static T[] GetComponents<T>(this IList<GameObject> gameObjects) where T : Component {
			T[] componentArray = new T[gameObjects.Count];
		
			for (int i = 0; i < gameObjects.Count; i++) {
				componentArray[i] = gameObjects[i].GetComponent<T>();
			}
			
			return componentArray;
		}
	}
}
