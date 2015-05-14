using UnityEngine;
using System.Collections;

public enum Wizardz {
	Harry = 0,
	Rincewind = 1,
	Gandalf = 2,
	Mysterio = 3,
}

public enum Spellz {
	SummonBlock,
	GravityWell,
	IceRain,
	MirrorBall
}

public static class WizardPlayerInfo {
	
	public static WizardInfo harry;
	public static WizardInfo rincewind;
	public static WizardInfo gandalf;
	public static WizardInfo mysterio;
	public static PlayerInfo[] players = { null, null, null, null };
	
	static int playerCount = 0;
	
	static WizardPlayerInfo() {
		harry = new WizardInfo(Wizardz.Harry);
		rincewind = new WizardInfo(Wizardz.Rincewind);
		gandalf = new WizardInfo(Wizardz.Gandalf);
		mysterio = new WizardInfo(Wizardz.Mysterio);
//		whether this is on will be determined by if we want one player to be active when the wizard select screen comes up
//		PlayerInfo[0] = new PlayerInfo(harry, 0);
//		playerCount++;
	}
	
	public static WizardInfo NextWizard(WizardInfo currentWizard) {
		int index = (int)currentWizard.index;
		index++;
		if (index >= 3) {
			index = 0;
		}
		return GetWizardInfo(index);
	}
	
	public static WizardInfo PreviousWizard(WizardInfo currentWizard) {
		int index = (int)currentWizard.index;
		index--;
		if (index < 0) {
			index = 3;
		}
		return GetWizardInfo(index);
	}
	
	public static WizardInfo GetWizardInfo(Wizardz wizard) {
		return GetWizardInfo((int)wizard);
	}
	
	public static WizardInfo GetWizardInfo(int index) {
		switch (index) {
			case 0:
				return harry;
			case 1:
				return rincewind;
			case 2:
				return gandalf;
			case 3:
				return mysterio;
			default:
				return null;
		}
	}
	
	public static void SelectWizard(WizardInfo wizard) {
		if (WizardIsTaken(wizard)) {
			Debug.LogError("This wizard is already chosen X_X");
			return;
		}
		
		wizard.chosen = true;
	}
	
	public static PlayerInfo NewPlayer() {
		int index = playerCount;
		WizardInfo wizard = GetWizardInfo(index);
		int counter = 0;
		
		while (WizardIsTaken(wizard)) {
			wizard = NextWizard(wizard);
			counter++;
			if (counter > 5) {
				Debug.LogError("Way too many wizardz :S");
				break;
			}
		}
		
		players[index] = new PlayerInfo(wizard, index);
		
		playerCount++;
		
		return players[index];
	}

	static bool WizardIsTaken(WizardInfo wizard) {
		return wizard.chosen;
	}
	
	public static PlayerInfo GetPlayerBasedOnWizard(Wizardz wizard) {
		foreach (PlayerInfo player in players) {
			if (player == null) continue;
			
			if (player.wizard.wizNum == wizard) {
				return player;
			}
		}
		
		Debug.LogWarning("There's no wizards of that name!");
		return null;
	}
}

public class WizardInfo {
	
	public int index;
	public Wizardz wizNum;
	public Spellz spell;
	public string anim;
	public bool chosen = false;
	
	public WizardInfo(Wizardz wizard) {
		
		index = (int)wizard;
		anim = "Menu" + wizNum.ToString();
		wizNum = wizard;
		
		switch (wizard) {
			case Wizardz.Harry:
				spell = Spellz.SummonBlock;
				break;
			case Wizardz.Rincewind:
				spell = Spellz.GravityWell;
				break;
			case Wizardz.Gandalf:
				spell = Spellz.IceRain;
				break;
			case Wizardz.Mysterio:
				spell = Spellz.MirrorBall;
				break;
		}
	}
	
	public override string ToString() {
		return wizNum.ToString();
	}
}

public class PlayerInfo {
	
	public int playerNo;
	
	public WizardInfo wizard;
	
	public Spellz spell {
		get {
			return wizard.spell;
		}
	}
	
	public PlayerInfo(WizardInfo wizard, int no) {
		this.wizard = wizard;
		this.playerNo = no;
	}
	
	public override string ToString() {
		return "Player " + (playerNo + 1) + " with " + spell;
	}
}
