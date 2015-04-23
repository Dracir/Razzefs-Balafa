using UnityEngine;
using System;
using System.Collections;

namespace Magicolo {
	public static class DoubleExtensions {

		public static double PowSign(this double d, double power) {
			return Math.Abs(d).Pow(power) * d.Sign();
		}
	
		public static double PowSign(this double d) {
			return d.PowSign(2);
		}
	
		public static double Pow(this double d, double power) {
			if (double.IsNaN(d)) {
				return 0;
			}
			
			if (power == 0) {
				return 1;
			}
			
			if (power == 1) {
				return d;
			}
			
			return Math.Pow(d, power);
		}
	
		public static double Pow(this double d) {
			return d.Pow(2);
		}
	
		public static double Round(this double d, double step) {
			if (double.IsNaN(d)) {
				return 0;
			}
		
			if (step <= 0) {
				return d;
			}
		
			return Math.Round(d * (1D / step)) / (1D / step);
		}
	
		public static double Round(this double d) {
			return d.Round(1);
		}
		
		public static double Wrap(this double d, double wrap) {
			if (wrap <= 0) {
				return d;
			}
			
			while (d < 0) {
				d += wrap;
			}
			
			while (d > wrap) {
				d -= wrap;
			}
			
			return d;
		}
		
		public static int Sign(this double d) {
			return d < 0 ? -1 : 1;
		}
	}
}
