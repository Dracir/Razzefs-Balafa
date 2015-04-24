using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;
using Magicolo.GeneralTools;

namespace Magicolo {
	public interface IStateMachine : IStateMachineCallable, IStateMachineLayerable {

		bool Debug { get ; }
		bool IsActive { get ; }
		
		IStateLayer[] GetLayers();
	}
}