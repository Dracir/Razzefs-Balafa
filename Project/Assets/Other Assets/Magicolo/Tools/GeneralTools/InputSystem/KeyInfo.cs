using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.GeneralTools;

namespace Magicolo {
	[System.Serializable]
	public class KeyInfo : INamable {

		[SerializeField]
		string name = "";
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}
    	
		[SerializeField] List<KeyCode> inputKeys = new List<KeyCode>();
		
		[SerializeField] List<MonoBehaviour> listenerReferences = new List<MonoBehaviour>();
		
		List<IInputKeyListener> listeners = new List<IInputKeyListener>();
    	
		public KeyInfo(string name, KeyCode[] keys, IInputKeyListener[] listeners) {
			this.name = name;
			
			SetKeys(keys);
			SetListeners(listeners);
		}

		public KeyInfo(string name, KeyCode[] keys) {
			this.name = name;
			
			SetKeys(keys);
		}

		public KeyCode[] GetKeys() {
			return inputKeys.ToArray();
		}
		
		public void SetKeys(KeyCode[] keys) {
			inputKeys = new List<KeyCode>(keys);
		}
		
		public void AddKey(KeyCode key) {
			inputKeys.Add(key);
		}
		
		public void RemoveKey(KeyCode key) {
			inputKeys.Remove(key);
		}
	
		public IInputKeyListener[] GetListeners() {
			return listeners.ToArray();
		}
		
		public void SetListeners(IInputKeyListener[] listeners) {
			this.listeners = new List<IInputKeyListener>(listeners);
		}
		
		public void SetListeners() {
			listeners = new List<IInputKeyListener>();
			
			foreach (MonoBehaviour listenerReference in listenerReferences) {
				IInputKeyListener listener = listenerReference as IInputKeyListener;
				
				if (listener != null) {
					listeners.Add(listener);
				}
			}
		}
		
		public void AddListener(IInputKeyListener listener) {
			if (!listeners.Contains(listener)) {
				listeners.Add(listener);
			}
		}
		
		public void RemoveListener(IInputKeyListener listener) {
			listeners.Remove(listener);
		}
		
		public bool HasListeners() {
			return listeners.Count > 0;
		}
		
		public override string ToString() {
			return string.Format("{0}({1}, {2}, {3})", GetType().Name, Name, Logger.ToString(inputKeys), Logger.ToString(listeners));
		}
	}
}