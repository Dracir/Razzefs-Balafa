using UnityEngine;
using Magicolo;


public class GameNextLevel : State {
	
    Game Layer {
    	get { return ((Game)layer); }
    }
    
    StateMachine Machine {
    	get { return ((StateMachine)machine); }
    }
	
	
	// Fix pour que le update se fasse apres le start car tser unity orders stuff et state machin stuff order suff words
	bool asAwake;
	
	public override void OnStart() {
		base.OnStart();
		DontDestroyOnLoad(this);
		Layer.levelCycle.loadMapPack();
		asAwake = true;
	}
	
	public override void OnEnter(){
		base.OnEnter();
	}
	
	public override void OnUpdate() {
		base.OnUpdate();
		if(asAwake){
			Layer.levelCycle.nextMap();
			SwitchState<GameLoadingLevel>();
		}
	}
}
