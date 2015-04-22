using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BOGButton : MonoBehaviour {
	
	public BOGPanel panel;
	public string actionCommand;

	public void makeUnityButtonConnection(string command) {
		Button button = GetComponent<Button>();
		
		actionCommand = command;
		
		UnityEditor.Events.UnityEventTools.AddVoidPersistentListener(button.onClick, () => panel.handleCommand(actionCommand));
	}
	
	void Start () {
	
	}
	
	
	void Update () {
	
	}
}
