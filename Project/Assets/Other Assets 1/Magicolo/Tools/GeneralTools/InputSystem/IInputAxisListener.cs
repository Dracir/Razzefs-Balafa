using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Magicolo;

namespace Magicolo {
	public interface IInputAxisListener {

		void OnAxisInput(AxisInfo axisInfo, float axisValue);
	}
}