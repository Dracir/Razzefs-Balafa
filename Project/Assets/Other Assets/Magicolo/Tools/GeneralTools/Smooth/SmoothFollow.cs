using UnityEngine;

namespace Magicolo {
	[AddComponentMenu("Magicolo/Smooth/Follow")]
	public class SmoothFollow : MonoBehaviourExtended {
	
		[Mask] public TransformModes mode = TransformModes.Position;
		[Mask(Axes.XYZ, AfterSeparator = true)] public Axes axis = Axes.XYZ;
		
		public Transform target;
		public Vector3 offset;
		[Clamp(0, 100)] public Vector3 damping = new Vector3(100, 100, 100);
	
		void FixedUpdate() {
			if (mode == TransformModes.None || axis == Axes.None) {
				return;
			}
			
			if (mode.Contains(TransformModes.Position)) {
				Vector3 position = transform.position;
			
				position.x = axis.Contains(Axes.X) ? damping.x >= 100 ? target.position.x + offset.x : Mathf.Lerp(position.x, target.position.x + offset.x, damping.x * Time.fixedDeltaTime) : position.x;
				position.y = axis.Contains(Axes.Y) ? damping.y >= 100 ? target.position.y + offset.y : Mathf.Lerp(position.y, target.position.y + offset.y, damping.y * Time.fixedDeltaTime) : position.y;
				position.z = axis.Contains(Axes.Z) ? damping.z >= 100 ? target.position.z + offset.z : Mathf.Lerp(position.z, target.position.z + offset.z, damping.z * Time.fixedDeltaTime) : position.z;
			
				transform.position = position;
			}
			
			if (mode.Contains(TransformModes.Rotation)) {
				Vector3 eulerAngles = transform.eulerAngles;
			
				eulerAngles.x = axis.Contains(Axes.X) ? damping.x >= 100 ? target.eulerAngles.x + offset.x : Mathf.Lerp(eulerAngles.x, target.eulerAngles.x + offset.x, damping.x * Time.fixedDeltaTime) : eulerAngles.x;
				eulerAngles.y = axis.Contains(Axes.Y) ? damping.y >= 100 ? target.eulerAngles.y + offset.y : Mathf.Lerp(eulerAngles.y, target.eulerAngles.y + offset.y, damping.y * Time.fixedDeltaTime) : eulerAngles.y;
				eulerAngles.z = axis.Contains(Axes.Z) ? damping.z >= 100 ? target.eulerAngles.z + offset.z : Mathf.Lerp(eulerAngles.z, target.eulerAngles.z + offset.z, damping.z * Time.fixedDeltaTime) : eulerAngles.z;
			
				transform.eulerAngles = eulerAngles;
			}
			
			if (mode.Contains(TransformModes.Scale)) {
				Vector3 scale = transform.lossyScale;
			
				scale.x = axis.Contains(Axes.X) ? damping.x >= 100 ? target.lossyScale.x + offset.x : Mathf.Lerp(scale.x, target.lossyScale.x + offset.x, damping.x * Time.fixedDeltaTime) : scale.x;
				scale.y = axis.Contains(Axes.Y) ? damping.y >= 100 ? target.lossyScale.y + offset.y : Mathf.Lerp(scale.y, target.lossyScale.y + offset.y, damping.y * Time.fixedDeltaTime) : scale.y;
				scale.z = axis.Contains(Axes.Z) ? damping.z >= 100 ? target.lossyScale.z + offset.z : Mathf.Lerp(scale.z, target.lossyScale.z + offset.z, damping.z * Time.fixedDeltaTime) : scale.z;
			
				transform.SetScale(scale);
			}
		}
	}
}
