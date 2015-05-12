using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ElderSignManager {

	//TODO: Maybe not this
	const int ElderSignStartCount = 0;

	static ElderSignManager(){
		WizardElderSignCount = new Dictionary<Wizardz, int> ();
		foreach(Wizardz wizard in System.Enum.GetValues(typeof(Wizardz))){
			WizardElderSignCount[wizard] = 0;
		}
	}

	static Dictionary<Wizardz,int> WizardElderSignCount;

	public static void incrementElderSignCount(Wizardz wizard){
		WizardElderSignCount[wizard]++;
	}

	public static void setElderSignCount(Wizardz wizard, int count){
		WizardElderSignCount[wizard] = count;
	}

	public static int getElderSignCount(Wizardz wizard){
		return WizardElderSignCount[wizard];
	}

	public static int getTotalElderSignCount(){
		int total = 0;
		foreach(Wizardz wizard in System.Enum.GetValues(typeof(Wizardz))){
			total += WizardElderSignCount[wizard];
		}
		return total;
	}

}
