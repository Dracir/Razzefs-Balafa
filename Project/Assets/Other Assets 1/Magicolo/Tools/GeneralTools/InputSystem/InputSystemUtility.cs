using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo.GeneralTools {
	public static class InputSystemUtility {

		static Dictionary<string, IInputKeyListener> nameKeyListenerDict;
		public static Dictionary<string, IInputKeyListener> NameKeyListenerDict {
			get {
				if (nameKeyListenerDict == null) {
					BuildDicts();
				}
				
				return nameKeyListenerDict;
			}
		}
		
		static Dictionary<string, IInputAxisListener> nameAxisListenerDict;
		public static Dictionary<string, IInputAxisListener> NameAxisListenerDict {
			get {
				if (nameAxisListenerDict == null) {
					BuildDicts();
				}
				
				return nameAxisListenerDict;
			}
		}
		
		static Dictionary<string, IInputListener> nameListenerDict;
		public static Dictionary<string, IInputListener> NameListenerDict {
			get {
				if (nameListenerDict == null) {
					BuildDicts();
				}
				
				return nameListenerDict;
			}
		}
		
		public static string FormatListener(MonoBehaviour listener) {
			return string.Format("{0}/{1}", listener.gameObject.name, listener.GetType().Name.Split('.').Last());
		}
		
		public static void BuildDicts() {
			nameKeyListenerDict = new Dictionary<string, IInputKeyListener>();
			nameAxisListenerDict = new Dictionary<string, IInputAxisListener>();
			
			foreach (MonoBehaviour script in Object.FindObjectsOfType<MonoBehaviour>()) {
				IInputKeyListener keyListener = script as IInputKeyListener;
				IInputAxisListener axisListener = script as IInputAxisListener;
				IInputListener listener = script as IInputListener;
				
				if (keyListener != null) {
					nameKeyListenerDict[FormatListener(script)] = keyListener;
				}
				
				if (axisListener != null) {
					nameAxisListenerDict[FormatListener(script)] = axisListener;
				}
				
				if (listener != null) {
					nameListenerDict[FormatListener(script)] = listener;
				}
			}
		}
		
		#if UNITY_EDITOR
		[UnityEditor.Callbacks.DidReloadScripts]
		static void OnReloadScripts() {
			nameKeyListenerDict = null;
			nameAxisListenerDict = null;
			nameListenerDict = null;
		}
		#endif
	}
}