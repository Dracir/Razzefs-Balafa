using UnityEngine;
using System.Collections;

namespace Magicolo {
	public static class FloatExtensions {

		public static float PowSign(this float f, float power) {
			return Mathf.Abs(f).Pow(power) * f.Sign();
		}
	
		public static float PowSign(this float f) {
			return f.PowSign(2);
		}
	
		public static float Pow(this float f, float power) {
			if (float.IsNaN(f)){
				return 0;
			}
			
			if (power == 0) {
				return 1;
			}
			
			if (power == 1) {
				return f;
			}
			
			if (power == 2) {
				return f * f;
			}
			
			return Mathf.Pow(f, power);
		}
	
		public static float Pow(this float f) {
			return f.Pow(2);
		}
	
		public static float Round(this float f, float step) {
			if (float.IsNaN(f)) {
				return 0;
			}
		
			if (step <= 0) {
				return f;
			}
		
			return Mathf.Round(f * (1F / step)) / (1F / step);
		}
	
		public static float Round(this float f) {
			return f.Round(1);
		}
		
		public static float Wrap(this float f, float wrap) {
			if (wrap <= 0) {
				return f;
			}
			
			while (f < 0) {
				f += wrap;
			}
			
			while (f >= wrap) {
				f -= wrap;
			}
			
			return f;
		}
		
		public static int Sign(this float f) {
			return f < 0 ? -1 : 1;
		}
	}
}
