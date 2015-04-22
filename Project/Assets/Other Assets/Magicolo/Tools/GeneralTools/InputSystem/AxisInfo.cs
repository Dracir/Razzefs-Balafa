using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.GeneralTools;

namespace Magicolo {
	[System.Serializable]
	public class AxisInfo : INamable {

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
    	
		[SerializeField]
		string inputAxis;
		
		[SerializeField, PropertyField(typeof(RangeAttribute), 0, 1)]
		float threshold;
		public float Threshold {
			get {
				return threshold;
			}
			set {
				threshold = value;
			}
		}
		
		[SerializeField] List<MonoBehaviour> listenerReferences = new List<MonoBehaviour>();
		
		List<IInputAxisListener> listeners = new List<IInputAxisListener>();
    	
		public AxisInfo(string name, string axisName, float threshold, IInputAxisListener[] listeners) {
			Name = name;
			this.inputAxis = axisName;
			Threshold = threshold;
			
			SetListeners(listeners);
		}
		
		public AxisInfo(string name, string axisName, float threshold) {
			Name = name;
			this.inputAxis = axisName;
			Threshold = threshold;
		}
		
		public AxisInfo(string name, string axisName, IInputAxisListener[] listeners) {
			Name = name;
			this.inputAxis = axisName;
			
			SetListeners(listeners);
		}
		
		public AxisInfo(string name, string axisName) {
			Name = name;
			this.inputAxis = axisName;
		}
		
		public string GetAxis() {
			return inputAxis;
		}
		
		public void SetAxis(string axis) {
			inputAxis = axis;
		}

		public IInputAxisListener[] GetListeners() {
			return listeners.ToArray();
		}
		
		public void SetListeners(IInputAxisListener[] listeners) {
			this.listeners = new List<IInputAxisListener>(listeners);
		}
		
		public void SetListeners() {
			listeners = new List<IInputAxisListener>();
			
			foreach (MonoBehaviour listenerReference in listenerReferences) {
				IInputAxisListener listener = listenerReference as IInputAxisListener;
				
				if (listener != null) {
					listeners.Add(listener);
				}
			}
		}
		
		public void AddListener(IInputAxisListener listener) {
			listeners.Add(listener);
		}
		
		public void RemoveListener(IInputAxisListener listener) {
			listeners.Remove(listener);
		}
		
		public bool HasListeners() {
			return listeners.Count > 0;
		}
		
		public override string ToString() {
			return string.Format("{0}({1}, {2}, {3})", GetType().Name, Name, inputAxis, Threshold);
		}
	}
}