using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo {
	public interface IInputKeyListener {

		void OnKeyInput(KeyInfo keyInfo, KeyStates keyState);
	}
}