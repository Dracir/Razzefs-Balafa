using UnityEngine;
using System.Collections;

namespace Magicolo {
	public static class RectExtensions {

		public static Rect Round(this Rect rect, float step) {
			if (step <= 0) {
				return rect;
			}
			
			rect.x = rect.x.Round(step);
			rect.y = rect.y.Round(step);
			rect.width = rect.width.Round(step);
			rect.height = rect.height.Round(step);
			
			return rect;
		}
	
		public static Rect Round(this Rect rect) {
			return rect.Round(1);
		}
	
		public static bool Intersects(this Rect rect, Rect otherRect) {
			return !((rect.x > otherRect.xMax) || (rect.xMax < otherRect.x) || (rect.y > otherRect.yMax) || (rect.yMax < otherRect.y));
		}
	
		public static Rect Intersect(this Rect rect, Rect otherRect) {
			float x = Mathf.Max((sbyte)rect.x, (sbyte)otherRect.x);
			float num2 = Mathf.Min(rect.x + rect.width, otherRect.x + otherRect.width);
			float y = Mathf.Max((sbyte)rect.y, (sbyte)otherRect.y);
			float num4 = Mathf.Min(rect.y + rect.height, otherRect.y + otherRect.height);
			
			if ((num2 >= x) && (num4 >= y)) {
				return new Rect(x, y, num2 - x, num4 - y);
			}

			return new Rect();
		}
	}
}
