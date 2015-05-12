using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Magicolo.GeneralTools;

public class WizardSelect : MonoBehaviour {
	
	WizardInfo currentWizard;
	PlayerInfo myPlayer;
	
	public Animator anim;
	public Text wizardName;
	public Text spellName;
	
	const string spellPrefix = "	";
	string spellSuffix = ";D" + System.Environment.NewLine + "}";
	
	const string wizardPrefix = "Wizard.";
	const string wizardSuffix = "{";
	
	string FormattedSpellName {
		get {
			if (currentWizard == null) {
				return "nowizardplz";
			}
			return spellPrefix + currentWizard.spell + spellSuffix;
		}
	}
	
	string FormattedWizardName {
		get {
			if (currentWizard == null) {
				return "Stillnowizard";
			}
			return wizardPrefix + currentWizard.wizNum + wizardSuffix;
		}
	}
	
	void Update() {
		ControllerInfo controller = InputManager.GetNewController();
		
		if (controller != null) {
			Join(0);
			InputManager.AssignController(currentWizard.wizNum, controller);
			Logger.Log("New playah added!", currentWizard.wizNum, controller);
		}
	}
	
	void Join(int playerIndex) {
		myPlayer = WizardPlayerInfo.NewPlayer();
		currentWizard = myPlayer.wizard;
		
		//TODO change animation
		wizardName.text = FormattedWizardName;
		spellName.text = FormattedSpellName;
		Debug.Log("Got it ;)");
	}
}
