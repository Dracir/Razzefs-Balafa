using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Magicolo;

public class CharacterSelect : StateLayer {
	
	[SerializeField, PropertyField]
	Wizardz wizard;
	public Wizardz Wizard {
		get {
			return wizard;
		}
		set {
			wizard = value;
			
			wizardAnimator.SetFloat(wizardIndexHash, (int)wizard);
			wizardName.text = CharacterSelectMenu.GetFormattedWizardName(wizard);
			spellName.text = CharacterSelectMenu.GetFormattedSpellName(wizard);
		}
	}
	
	public Color selectingColor = new Color(1, 1, 1, 1);
	public Color readyColor = new Color(0, 1, 0, 1);
	
	public Animator wizardAnimator;
	public Text wizardName;
	public Text spellName;
	public Image background;
	public GameObject box;
	public CharacterSelectMenu selectMenu;
	public Text joinText;
	
	[Disable] public int wizardIndexHash = Animator.StringToHash("WizardIndex");
	
	bool _inputSystemCached;
	InputSystem _inputSystem;
	public InputSystem inputSystem { 
		get { 
			_inputSystem = _inputSystemCached ? _inputSystem : GetComponent<InputSystem>();
			_inputSystemCached = true;
			return _inputSystem;
		}
	}
	
	StateMachine Machine {
		get { return (StateMachine)machine; }
	}
}
