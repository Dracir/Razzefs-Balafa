using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo {
	[AddComponentMenu("Magicolo/General/Input System")]
	public class InputSystem : MonoBehaviourExtended {

		[SerializeField] List<KeyInfo> keyInfos = new List<KeyInfo>();
		[SerializeField] List<AxisInfo> axisInfos = new List<AxisInfo>();
		
		Dictionary<string, KeyInfo> nameKeyInfosDict;
		Dictionary<string, KeyInfo> NameKeyInfosDict {
			get {
				if (nameKeyInfosDict == null) {
					BuildKeyInfosDict();
				}
				
				return nameKeyInfosDict;
			}
		}

		Dictionary<string, AxisInfo> nameAxisInfosDict;
		Dictionary<string, AxisInfo> NameAxisInfosDict {
			get {
				if (nameAxisInfosDict == null) {
					BuildAxisInfosDict();
				}
				
				return nameAxisInfosDict;
			}
		}

		Dictionary<AxisInfo, float> axisInfoValueDict;
		Dictionary<AxisInfo, float> AxisInfoValueDict {
			get {
				if (axisInfoValueDict == null) {
					BuildAxisInfosDict();
				}
				
				return axisInfoValueDict;
			}
		}
		
		void Awake() {
			InitializeKeys();
			InitializeAxis();
		}
		
		void Update() {
			UpdateKeys();
			UpdateAxis();
		}

		void UpdateKeys() {
			foreach (KeyInfo keyInfo in keyInfos) {
				if (!keyInfo.HasListeners()) {
					continue;
				}
				
				foreach (KeyCode key in keyInfo.GetKeys()) {
					if (key == KeyCode.None) {
						continue;
					}
					
					if (Input.GetKeyDown(key)) {
						SendInput(keyInfo, KeyStates.Down);
						break;
					}
					
					if (Input.GetKeyUp(key)) {
						SendInput(keyInfo, KeyStates.Up);
						break;
					}
					
					if (Input.GetKey(key)) {
						SendInput(keyInfo, KeyStates.Pressed);
						break;
					}
				}
			}
		}

		void UpdateAxis() {
			foreach (AxisInfo axisInfo in axisInfos) {
				string axis = axisInfo.GetAxis();
				bool axisIsValid = !string.IsNullOrEmpty(axis) && axis != " ";
				
				if (!axisInfo.HasListeners() || !axisIsValid) {
					continue;
				}
				
				float currentValue = Input.GetAxis(axis);
				float lastValue = AxisInfoValueDict[axisInfo];
				
				if ((lastValue != 0 && currentValue == 0) || Mathf.Abs(currentValue - lastValue) > axisInfo.Threshold) {
					AxisInfoValueDict[axisInfo] = currentValue;
					SendInput(axisInfo, currentValue);
				}
			}
		}
		
		public void AddKeyInfo(KeyInfo keyInfo) {
			NameKeyInfosDict[keyInfo.Name] = keyInfo;
			keyInfos.Add(keyInfo);
		}
		
		public void RemoveKeyInfo(KeyInfo keyInfo) {
			NameKeyInfosDict.Remove(keyInfo.Name);
			keyInfos.Remove(keyInfo);
		}

		public KeyInfo GetKeyInfo(string keyInfoName) {
			KeyInfo keyInfo = null;
			
			try {
				keyInfo = NameKeyInfosDict[keyInfoName];
			}
			catch {
				Logger.LogError(string.Format("KeyInfo named {0} was not found.", keyInfoName));
			}
			
			return keyInfo;
		}
		
		public KeyInfo[] GetKeyInfos() {
			return keyInfos.ToArray();
		}
		
		public void AddAxisInfo(AxisInfo axisInfo) {
			NameAxisInfosDict[axisInfo.Name] = axisInfo;
			AxisInfoValueDict[axisInfo] = 0;
			axisInfos.Add(axisInfo);
		}
		
		public void RemoveAxisInfo(AxisInfo axisInfo) {
			NameAxisInfosDict.Remove(axisInfo.Name);
			AxisInfoValueDict.Remove(axisInfo);
			axisInfos.Remove(axisInfo);
		}
		
		public AxisInfo GetAxisInfo(string axisInfoName) {
			AxisInfo axisInfo = null;
			
			try {
				axisInfo = NameAxisInfosDict[axisInfoName];
			}
			catch {
				Logger.LogError(string.Format("AxisInfo named {0} was not found.", axisInfoName));
			}
			
			return axisInfo;
		}
		
		public AxisInfo[] GetAxisInfos() {
			return axisInfos.ToArray();
		}
		
		public void SimulateInput(string keyInfoName, KeyStates keyState) {
			SimulateInput(GetKeyInfo(keyInfoName), keyState);
		}
		
		public void SimulateInput(KeyInfo keyInfo, KeyStates keyState) {
			SendInput(keyInfo, keyState);
		}
		
		public void SimulateInput(string axisInfoName, float axisValue) {
			SendInput(GetAxisInfo(axisInfoName), axisValue);
		}
		
		public void SimulateInput(AxisInfo axisInfo, float axisValue) {
			SendInput(axisInfo, axisValue);
		}
		
		void SendInput(KeyInfo keyInfo, KeyStates keyState) {
			foreach (IInputKeyListener listener in keyInfo.GetListeners()) {
				listener.OnKeyInput(keyInfo, keyState);
			}
		}
		
		void SendInput(AxisInfo axisInfo, float axisValue) {
			foreach (IInputAxisListener listener in axisInfo.GetListeners()) {
				listener.OnAxisInput(axisInfo, axisValue);
			}
		}

		void InitializeKeys() {
			foreach (KeyInfo keyInfo in keyInfos) {
				keyInfo.SetListeners();
			}
		}
		
		void InitializeAxis() {
			foreach (AxisInfo axisInfo in axisInfos) {
				axisInfo.SetListeners();
			}
		}
		
		void BuildKeyInfosDict() {
			nameKeyInfosDict = new Dictionary<string, KeyInfo>();
			
			foreach (KeyInfo keyInfo in keyInfos) {
				nameKeyInfosDict[keyInfo.Name] = keyInfo;
			}
		}

		void BuildAxisInfosDict() {
			nameAxisInfosDict = new Dictionary<string, AxisInfo>();
			axisInfoValueDict = new Dictionary<AxisInfo, float>();
			
			foreach (AxisInfo axisInfo in axisInfos) {
				nameAxisInfosDict[axisInfo.Name] = axisInfo;
				axisInfoValueDict[axisInfo] = 0;
			}
		}
	}
}