using UnityEngine;
using System.Collections;

namespace Magicolo {
	public static class Vector2Extensions {
		
		const float epsilon = 0.001F;
		
		public static Vector2 SetValues(this Vector2 vector, Vector2 values, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? values.x : vector.x;
			vector.y = axis.Contains(Axes.Y) ? values.y : vector.y;
			
			return vector;
		}
		
		public static Vector2 SetValues(this Vector2 vector, Vector2 values) {
			return vector.SetValues(values, Axes.XYZW);
		}
				
		public static Vector2 Lerp(this Vector2 vector, Vector2 target, float time, Axes axis) {
			vector.x = axis.Contains(Axes.X) && Mathf.Abs(target.x - vector.x) > epsilon ? Mathf.Lerp(vector.x, target.x, time) : vector.x;
			vector.y = axis.Contains(Axes.Y) && Mathf.Abs(target.y - vector.y) > epsilon ? Mathf.Lerp(vector.y, target.y, time) : vector.y;
			
			return vector;
		}
			
		public static Vector2 Lerp(this Vector2 vector, Vector2 target, float time) {
			return vector.Lerp(target, time, Axes.XYZW);
		}
		
		public static Vector2 LerpLinear(this Vector2 vector, Vector2 target, float time, Axes axis) {
			Vector2 difference = target - vector;
			Vector2 direction = Vector2.zero.SetValues(difference, axis);
			float distance = direction.magnitude;
					
			Vector2 adjustedDirection = direction.normalized * time;
					
			if (adjustedDirection.magnitude < distance) {
				vector += Vector2.zero.SetValues(adjustedDirection, axis);
			}
			else {
				vector = vector.SetValues(target, axis);
			}
			
			return vector;
		}
		
		public static Vector2 LerpLinear(this Vector2 vector, Vector2 target, float time) {
			return vector.LerpLinear(target, time, Axes.XYZW);
		}

		public static Vector2 LerpAngles(this Vector2 vector, Vector2 targetAngles, float time, Axes axis) {
			vector.x = axis.Contains(Axes.X) && Mathf.Abs(targetAngles.x - vector.x) > epsilon ? Mathf.LerpAngle(vector.x, targetAngles.x, time) : vector.x;
			vector.y = axis.Contains(Axes.Y) && Mathf.Abs(targetAngles.y - vector.y) > epsilon ? Mathf.LerpAngle(vector.y, targetAngles.y, time) : vector.y;
			
			return vector;
		}

		public static Vector2 LerpAngles(this Vector2 vector, Vector2 targetAngles, float time) {
			return vector.LerpAngles(targetAngles, time, Axes.XYZW);
		}

		public static Vector2 LerpAnglesLinear(this Vector2 vector, Vector2 targetAngles, float time, Axes axis) {
			Vector2 difference = new Vector2(Mathf.DeltaAngle(vector.x, targetAngles.x), Mathf.DeltaAngle(vector.y, targetAngles.y));
			Vector2 direction = Vector2.zero.SetValues(difference, axis);
			float distance = direction.magnitude * Mathf.Rad2Deg;
					
			Vector2 adjustedDirection = direction.normalized * time;
					
			if (adjustedDirection.magnitude < distance) {
				vector += Vector2.zero.SetValues(adjustedDirection, axis);
			}
			else {
				vector = vector.SetValues(targetAngles, axis);
			}
			
			return vector;
		}
		
		public static Vector2 LerpAnglesLinear(this Vector2 vector, Vector2 targetAngles, float time) {
			return vector.LerpAnglesLinear(targetAngles, time, Axes.XYZW);
		}
		
		public static Vector2 Oscillate(this Vector2 vector, Vector2 frequency, Vector2 amplitude, Vector2 center, float offset, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? center.x + amplitude.x * Mathf.Sin(frequency.x * Time.time + offset) : vector.x;
			vector.y = axis.Contains(Axes.Y) ? center.y + amplitude.y * Mathf.Sin(frequency.y * Time.time + offset) : vector.y;
			
			return vector;
		}
		
		public static Vector2 Oscillate(this Vector2 vector, Vector2 frequency, Vector2 amplitude, Vector2 center, float offset) {
			return vector.Oscillate(frequency, amplitude, center, offset, Axes.XYZW);
		}
		
		public static Vector2 Oscillate(this Vector2 vector, Vector2 frequency, Vector2 amplitude, Vector2 center, Axes axis) {
			return vector.Oscillate(frequency, amplitude, center, 0, axis);
		}
		
		public static Vector2 Oscillate(this Vector2 vector, Vector2 frequency, Vector2 amplitude, Vector2 center) {
			return vector.Oscillate(frequency, amplitude, center, 0, Axes.XYZW);
		}

		public static Vector2 Mult(this Vector2 vector, Vector2 otherVector, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x * otherVector.x : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y * otherVector.y : vector.y;
			
			return vector;
		}
	
		public static Vector2 Mult(this Vector2 vector, Vector2 otherVector) {
			return vector.Mult(otherVector, Axes.XYZW);
		}
	
		public static Vector2 Mult(this Vector2 vector, Vector3 otherVector, Axes axis) {
			return vector.Mult((Vector2)otherVector, axis);
		}
	
		public static Vector2 Mult(this Vector2 vector, Vector3 otherVector) {
			return vector.Mult((Vector2)otherVector, Axes.XYZW);
		}
	
		public static Vector2 Mult(this Vector2 vector, Vector4 otherVector, Axes axis) {
			return vector.Mult((Vector2)otherVector, axis);
		}
	
		public static Vector2 Mult(this Vector2 vector, Vector4 otherVector) {
			return vector.Mult((Vector2)otherVector, Axes.XYZW);
		}
	
		public static Vector2 Div(this Vector2 vector, Vector2 otherVector, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x / otherVector.x : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y / otherVector.y : vector.y;
			
			return vector;
		}
	
		public static Vector2 Div(this Vector2 vector, Vector2 otherVector) {
			return vector.Div(otherVector, Axes.XYZW);
		}
	
		public static Vector2 Div(this Vector2 vector, Vector3 otherVector, Axes axis) {
			return vector.Div((Vector2)otherVector, axis);
		}
	
		public static Vector2 Div(this Vector2 vector, Vector3 otherVector) {
			return vector.Div((Vector2)otherVector, Axes.XYZW);
		}
	
		public static Vector2 Div(this Vector2 vector, Vector4 otherVector, Axes axis) {
			return vector.Div((Vector2)otherVector, axis);
		}
	
		public static Vector2 Div(this Vector2 vector, Vector4 otherVector) {
			return vector.Div((Vector2)otherVector, Axes.XYZW);
		}
	
		public static Vector2 Pow(this Vector2 vector, float power, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x.Pow(power) : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y.Pow(power) : vector.y;
			
			return vector;
		}
	
		public static Vector2 Pow(this Vector2 vector, float power) {
			return vector.Pow(power, Axes.XYZW);
		}
	
		public static Vector2 Round(this Vector2 vector, float step, Axes axis) {
			vector.x = axis.Contains(Axes.X) ? vector.x.Round(step) : vector.x;
			vector.y = axis.Contains(Axes.Y) ? vector.y.Round(step) : vector.y;
			
			return vector;
		}
	
		public static Vector2 Round(this Vector2 vector, float step) {
			return vector.Round(step, Axes.XYZW);
		}
	
		public static Vector2 Round(this Vector2 vector) {
			return vector.Round(1, Axes.XYZW);
		}
	
		public static bool Intersects(this Vector2 vector, Rect rect) {
			return ((Vector3)vector).Intersects(rect);
		}
		
		public static Vector2 Rotate(this Vector2 vector, float angle) {
			return ((Vector3)vector).Rotate(angle);
		}
	
		public static Vector2 SquareClamp(this Vector2 vector, float size = 1) {
			return ((Vector3)vector).SquareClamp(size);
		}
	
		public static Vector2 RectClamp(this Vector2 vector, float width = 1, float height = 1) {
			float clamped;
		
			if (vector.x < -width || vector.x > width) {
				clamped = Mathf.Clamp(vector.x, -width, width);
				vector.y *= clamped / vector.x;
				vector.x = clamped;
			}
		
			if (vector.y < -height || vector.y > height) {
				clamped = Mathf.Clamp(vector.y, -height, height);
				vector.x *= clamped / vector.y;
				vector.y = clamped;
			}
		
			return vector;
		}
		
		public static float Angle(this Vector2 vector) {
			return (Vector2.Angle(Vector2.right, vector) * -vector.y.Sign()).Wrap(360);
		}
	}
}
