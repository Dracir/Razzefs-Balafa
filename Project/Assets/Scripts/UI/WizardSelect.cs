using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
		get{
			if (currentWizard == null){
				return "nowizardplz";
			}
			return spellPrefix + currentWizard.spell+ spellSuffix;
		}
	}
	
	string FormattedWizardName {
		get{
			if (currentWizard == null){
				return "Stillnowizard";
			}
			return wizardPrefix + currentWizard.wizNum + wizardSuffix;
		}
	}
	
	void Start () {
		Join(0);
	}
	
	void Update () {
		//TODO add input detection: join game: Call WizardPlayerInfo.NewPlayer();
		
		//TODO add input detection: next/prev wizard
	}
	
	void Join (int playerIndex) {
		myPlayer = WizardPlayerInfo.NewPlayer();
		currentWizard = myPlayer.wizard;
		
		//TODO change animation
		wizardName.text = FormattedWizardName;
		spellName.text = FormattedSpellName;
		Debug.Log("Got it ;)");
	}
}
