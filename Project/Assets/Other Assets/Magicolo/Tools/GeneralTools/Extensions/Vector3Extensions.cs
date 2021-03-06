﻿using UnityEngine;
using System.Collections;

namespace Magicolo {
	public static class Vector3Extensions {
		
		const float epsilon = 0.001F;
		
		public static Vector3 SetValues(this Vector3 vector, Vector3 values, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? values.x : vector.x;
			vector.y = axis.Contains(Axes.Y) ? values.y : vector.y;
			vector.z = axis.Contains(Axes.Z) ? values.z : vector.z;
			
			return vector;
		}
		
		public static Vector3 SetValues(this Vector3 vector, Vector3 values) {
			return vector.SetValues(values, Axes.XYZW);
		}
				
		public static Vector3 Lerp(this Vector3 vector, Vector3 target, float time, Axes axis) {
			vector.x = axis.Contains(Axes.X) && Mathf.Abs(target.x - vector.x) > epsilon ? Mathf.Lerp(vector.x, target.x, time) : vector.x;
			vector.y = axis.Contains(Axes.Y) && Mathf.Abs(target.y - vector.y) > epsilon ? Mathf.Lerp(vector.y, target.y, time) : vector.y;
			vector.z = axis.Contains(Axes.Z) && Mathf.Abs(target.z - vector.z) > epsilon ? Mathf.Lerp(vector.z, target.z, time) : vector.z;
			
			return vector;
		}
			
		public static Vector3 Lerp(this Vector3 vector, Vector3 target, float time) {
			return vector.Lerp(target, time, Axes.XYZW);
		}
		
		public static Vector3 LerpLinear(this Vector3 vector, Vector3 target, float time, Axes axis) {
			Vector3 difference = target - vector;
			Vector3 direction = Vector3.zero.SetValues(difference, axis);
			float distance = direction.magnitude;
					
			Vector3 adjustedDirection = direction.normalized * time;
					
			if (adjustedDirection.magnitude < distance) {
				vector += Vector3.zero.SetValues(adjustedDirection, axis);
			}
			else {
				vector = vector.SetValues(target, axis);
			}
			
			return vector;
		}
		
		public static Vector3 LerpLinear(this Vector3 vector, Vector3 target, float time) {
			return vector.LerpLinear(target, time, Axes.XYZW);
		}

		public static Vector3 LerpAngles(this Vector3 vector, Vector3 targetAngles, float time, Axes axis) {
			vector.x = axis.Contains(Axes.X) && Mathf.Abs(targetAngles.x - vector.x) > epsilon ? Mathf.LerpAngle(vector.x, targetAngles.x, time) : vector.x;
			vector.y = axis.Contains(Axes.Y) && Mathf.Abs(targetAngles.y - vector.y) > epsilon ? Mathf.LerpAngle(vector.y, targetAngles.y, time) : vector.y;
			vector.z = axis.Contains(Axes.Z) && Mathf.Abs(targetAngles.z - vector.z) > epsilon ? Mathf.LerpAngle(vector.z, targetAngles.z, time) : vector.z;
			
			return vector;
		}

		public static Vector3 LerpAngles(this Vector3 vector, Vector3 targetAngles, float time) {
			return vector.LerpAngles(targetAngles, time, Axes.XYZW);
		}

		public static Vector3 LerpAnglesLinear(this Vector3 vector, Vector3 targetAngles, float time, Axes axis) {
			Vector3 difference = new Vector3(Mathf.DeltaAngle(vector.x, targetAngles.x), Mathf.DeltaAngle(vector.y, targetAngles.y), Mathf.DeltaAngle(vector.z, targetAngles.z));
			Vector3 direction = Vector3.zero.SetValues(difference, axis);
			float distance = direction.magnitude * Mathf.Rad2Deg;
					
			Vector3 adjustedDirection = direction.normalized * time;
					
			if (adjustedDirection.magnitude < distance) {
				vector += Vector3.zero.SetValues(adjustedDirection, axis);
			}
			else {
				vector = vector.SetValues(targetAngles, axis);
			}
			
			return vector;
		}
		
		public static Vector3 LerpAnglesLinear(this Vector3 vector, Vector3 targetAngles, float time) {
			return vector.LerpAnglesLinear(targetAngles, time, Axes.XYZW);
		}
		
		public static Vector3 Oscillate(this Vector3 vector, Vector3 frequency, Vector3 amplitude, Vector3 center, float offset, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? center.x + amplitude.x * Mathf.Sin(frequency.x * Time.time + offset) : vector.x;
			vector.y = axis.Contains(Axes.Y) ? center.y + amplitude.y * Mathf.Sin(frequency.y * Time.time + offset) : vector.y;
			vector.z = axis.Contains(Axes.Z) ? center.z + amplitude.z * Mathf.Sin(frequency.z * Time.time + offset) : vector.z;
			
			return vector;
		}
		
		public static Vector3 Oscillate(this Vector3 vector, Vector3 frequency, Vector3 amplitude, Vector3 center, float offset) {
			return vector.Oscillate(frequency, amplitude, center, offset, Axes.XYZW);
		}
		
		public static Vector3 Oscillate(this Vector3 vector, Vector3 frequency, Vector3 amplitude, Vector3 center, Axes axis) {
			return vector.Oscillate(frequency, amplitude, center, 0, axis);
		}
		
		public static Vector3 Oscillate(this Vector3 vector, Vector3 frequency, Vector3 amplitude, Vector3 center) {
			return vector.Oscillate(frequency, amplitude, center, 0, Axes.XYZW);
		}

		public static Vector3 Mult(this Vector3 vector, Vector3 otherVector, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x * otherVector.x : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y * otherVector.y : vector.y;
			vector.z = axis.Contains(Axes.Z) ? vector.z * otherVector.z : vector.z;
			
			return vector;
		}
	
		public static Vector3 Mult(this Vector3 vector, Vector3 otherVector) {
			return vector.Mult(otherVector, Axes.XYZW);
		}
	
		public static Vector3 Mult(this Vector3 vector, Vector2 otherVector, Axes axis) {
			return vector.Mult((Vector3)otherVector, axis);
		}
	
		public static Vector3 Mult(this Vector3 vector, Vector2 otherVector) {
			return vector.Mult((Vector3)otherVector, Axes.XYZW);
		}
	
		public static Vector3 Mult(this Vector3 vector, Vector4 otherVector, Axes axis) {
			return vector.Mult((Vector3)otherVector, axis);
		}
	
		public static Vector3 Mult(this Vector3 vector, Vector4 otherVector) {
			return vector.Mult((Vector3)otherVector, Axes.XYZW);
		}
	
		public static Vector3 Div(this Vector3 vector, Vector3 otherVector, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x / otherVector.x : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y / otherVector.y : vector.y;
			vector.z = axis.Contains(Axes.Z) ? vector.z / otherVector.z : vector.z;
			
			return vector;
		}
	
		public static Vector3 Div(this Vector3 vector, Vector3 otherVector) {
			return vector.Div(otherVector, Axes.XYZW);
		}
	
		public static Vector3 Div(this Vector3 vector, Vector2 otherVector, Axes axis) {
			return vector.Div((Vector3)otherVector, axis);
		}
	
		public static Vector3 Div(this Vector3 vector, Vector2 otherVector) {
			return vector.Div((Vector3)otherVector, Axes.XYZW);
		}
	
		public static Vector3 Div(this Vector3 vector, Vector4 otherVector, Axes axis) {
			return vector.Div((Vector3)otherVector, axis);
		}
	
		public static Vector3 Div(this Vector3 vector, Vector4 otherVector) {
			return vector.Div((Vector3)otherVector, Axes.XYZW);
		}
	
		public static Vector3 Pow(this Vector3 vector, float power, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x.Pow(power) : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y.Pow(power) : vector.y;
			vector.z = axis.Contains(Axes.Z) ? vector.z.Pow(power) : vector.z;
			
			return vector;
		}
	
		public static Vector3 Pow(this Vector3 vector, float power) {
			return vector.Pow(power, Axes.XYZW);
		}
	
		public static Vector3 Round(this Vector3 vector, float step, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x.Round(step) : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y.Round(step) : vector.y;
			vector.z = axis.Contains(Axes.Z) ? vector.z.Round(step) : vector.z;
			
			return vector;
		}
	
		public static Vector3 Round(this Vector3 vector, float step) {
			return vector.Round(step, Axes.XYZW);
		}
	
		public static Vector3 Round(this Vector3 vector) {
			return vector.Round(1, Axes.XYZW);
		}
	
		public static float Average(this Vector3 vector, Axes axis) {
			float average = 0;
			int axisCount = 0;
		
			if (axis.Contains(Axes.X)) {
				average += vector.x;
				axisCount += 1;
			}
		
			if (axis.Contains(Axes.Y)) {
				average += vector.y;
				axisCount += 1;
			}
		
			if (axis.Contains(Axes.Z)) {
				average += vector.z;
				axisCount += 1;
			}
		
			return average / axisCount;
		}
	
		public static float Average(this Vector3 vector) {
			return ((Vector3)vector).Average(Axes.XYZW);
		}

		public static bool Intersects(this Vector3 vector, Rect rect) {
			return vector.x >= rect.xMin && vector.x <= rect.xMax && vector.y >= rect.yMin && vector.y <= rect.yMax;
		}
		
		public static Vector3 Rotate(this Vector3 vector, float angle) {
			return vector.Rotate(angle, Vector3.forward);
		}
	
		public static Vector3 Rotate(this Vector3 vector, float angle, Vector3 axis) {
			angle %= 360;
			return Quaternion.AngleAxis(-angle, axis) * vector;
		}
		
		public static Vector3 ClampMagnitude(this Vector3 vector, float min, float max) {
			Vector3 clamped = vector;
			float sqrMagniture = vector.sqrMagnitude;
			float sqrMin = min * min;
			float sqrMax = max * max;
			
			if (sqrMagniture < sqrMin) {
				clamped = vector.normalized * min;
			}
			else if (sqrMagniture > sqrMax) {
				clamped = vector.normalized * max;
			}
			
			return clamped;
		}
		
		public static Vector3 SquareClamp(this Vector3 vector, float size) {
			return vector.RectClamp(size, size);
		}
	
		public static Vector3 RectClamp(this Vector3 vector, float width, float height) {
			return ((Vector2)vector).RectClamp(width, height);
		}
	}
}
