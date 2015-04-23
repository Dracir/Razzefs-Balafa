﻿using UnityEngine;

namespace Magicolo {
	public static class IntExtensions {

		public static float PowSign(this int i, float power) {
			return Mathf.Abs(i).Pow(power) * i.Sign();
		}
	
		public static float PowSign(this int i) {
			return i.PowSign(2);
		}
	
		public static float Pow(this int i, float power) {
			if (power == 0) {
				return 1;
			}
			
			if (power == 1) {
				return i;
			}
			
			return Mathf.Pow(i, power);
		}
	
		public static float Pow(this int i) {
			return i.Pow(2);
		}
	
		public static int Round(this int i, float step) {
			if (step <= 0) {
				return i;
			}
		
			return (int)(Mathf.Round(i * (1F / step)) / (1F / step));
		}
	
		public static int Round(this int i) {
			return i.Round(1);
		}
		
		public static int Wrap(this int i, int wrap) {
			if (wrap <= 0) {
				return i;
			}
			
			while (i < 0) {
				i += wrap;
			}
			
			while (i > wrap) {
				i -= wrap;
			}
			
			return i;
		}
		
		public static int Sign(this int i) {
			return i < 0 ? -1 : 1;
		}
	}
}
