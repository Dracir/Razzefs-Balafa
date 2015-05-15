using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Magicolo;
using Magicolo.GeneralTools;

public class CharacterSelectMenu : MonoBehaviourExtended {
	
	public CharacterSelect[] characterBoxes;
	public GameObject[] characterPrefabs;
	
	[Disable] public bool skipUpdate;
	
	void Update() {
		if (skipUpdate) {
			skipUpdate = false;
			return;
		}
		
		if (!Array.TrueForAll(characterBoxes, characterBox => characterBox.box.activeSelf)) {
			ControllerInfo controller = InputManager.GetNewController();
		
			if (controller != null) {
				AddCharacter(GetAvailableCharacterBox(), controller);
			}
		}
	}
	
	public void TryStartGame() {
		if (Array.TrueForAll(characterBoxes, characterBox => !characterBox.box.activeSelf || characterBox.StateIsActive<CharacterSelectReady>())) {
			Game.instance.SwitchState<GameNextLevel>();
		}
	}
	
	public CharacterSelect GetAvailableCharacterBox() {
		foreach (CharacterSelect wizardBox in characterBoxes) {
			if (!wizardBox.box.activeSelf) {
				return wizardBox;
			}
		}
		
		return null;
	}
	
	public Wizardz GetNextAvailableWizard(Wizardz wizard) {
		int wizardIndex = ((int)wizard + 1).Wrap(4);

		for (int i = wizardIndex; i < wizardIndex + 4; i++) {
			Wizardz currentWizard = (Wizardz)i.Wrap(4);
			
			if (Array.TrueForAll(characterBoxes, characterBox => !characterBox.box.activeSelf || characterBox.Wizard != currentWizard)) {
				return currentWizard;
			}
		}
		
		return wizard;
	}
	
	public Wizardz GetPreviousAvailableWizard(Wizardz wizard) {
		int wizardIndex = ((int)wizard - 1).Wrap(4);

		for (int i = wizardIndex; i > wizardIndex - 4; i--) {
			Wizardz currentWizard = (Wizardz)i.Wrap(4);
			
			if (Array.TrueForAll(characterBoxes, characterBox => !characterBox.box.activeSelf || characterBox.Wizard != currentWizard)) {
				return currentWizard;
			}
		}
		
		return wizard;
	}
	
	public void AddCharacter(CharacterSelect characterBox, ControllerInfo controller) {
		if (characterBox == null) {
			Logger.LogError("TOO MUCH WIZARDS!!!");
		}
		
		characterBox.SwitchState<CharacterSelectSelecting>();
		characterBox.Wizard = GetNextAvailableWizard(Wizardz.Mysterio);
		InputManager.AssignController(characterBox.Wizard, controller);
		InputManager.SetController(characterBox.Wizard, characterBox.inputSystem);
		Game.instance.playersPrefab[characterBox.Id] = characterPrefabs[(int)characterBox.Wizard];
	}
	
	public static string GetFormattedWizardName(Wizardz wizard) {
		return "Wizard." + wizard + "{";
	}
	
	public static string GetFormattedSpellName(Wizardz wizard) {
		return "	" + wizard.ConvertByIndex<Spellz>() + ";D" + Environment.NewLine + "}";
	}
}
