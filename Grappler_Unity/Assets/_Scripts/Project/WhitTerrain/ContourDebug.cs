using UnityEngine;
using System.Collections;

namespace WhitTerrain {
	[RequireComponent(typeof(Contour))]
	public class ContourDebug : MonoBehaviour {
		public bool isOn = true;
		[Range(0.1f, 0.9f)] public float sphereRadius = 0.5f;

		private enum GizmoColorType {
			Color1,
			Color2
		}

		private Contour terrain;
		private GizmoColorType colorType = GizmoColorType.Color1;

		private void Awake() {
			terrain = GetComponent<Contour>();
		}

		private void OnDrawGizmos() {
			if (!ShouldDrawGizmos()) return;

			DrawGizmos();
		}

		private bool ShouldDrawGizmos() {
			return isOn && TerrainIsValid();
		}

		private bool TerrainIsValid() {
			return terrain != null && terrain.IsValid();
		}

		private void DrawGizmos() {
			for (int i = 0; i < terrain.sections.Count; i++) {
				if (i % 2 == 1) SetGizmoColorType1();
				else SetGizmoColorType2();
				UpdateGizmoColor();
				DrawGizmosForSection(terrain.sections[i], i == terrain.sections.Count - 1);
			}
		}

		private void DrawGizmosForSection(ContourSegment section, bool withEndPoint) {
			DrawGizmoForPoint(section.startPoint);
			foreach (Vector2 point in section.midPoints) DrawGizmoForPoint(point);
			if (withEndPoint) DrawGizmoForPoint(section.endPoint);
		}

		private void DrawGizmoForPoint(Vector2 point) {
			Vector3 point3D = transform.TransformPoint(new Vector3(point.x, point.y, 0));

			Gizmos.DrawSphere(point3D, sphereRadius);
		}

		private void SetGizmoColorType1() {
			colorType = GizmoColorType.Color1;
		}

		private void SetGizmoColorType2() {
			colorType = GizmoColorType.Color2;
		}

		private void UpdateGizmoColor() {
			if (colorType == GizmoColorType.Color1) Gizmos.color = Color.green;
			else if (colorType == GizmoColorType.Color2) Gizmos.color = Color.blue;
		}
	}
}