using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TerrainLine))]
public class TerrainLineDebug : MonoBehaviour {
	public bool isOn = true;

	private enum GizmoColorType {
		Color1,
		Color2
	}

	private TerrainLine terrainLine;
	private GizmoColorType colorType = GizmoColorType.Color1;

	private void Awake() {
		terrainLine = GetComponent<TerrainLine>();
	}

	private void OnDrawGizmos() {
		if (!ShouldDrawGizmos()) return;

		DrawGizmos();
	}

	private bool ShouldDrawGizmos() {
		return isOn && TerrainLineIsValid();
	}

	private bool TerrainLineIsValid() {
		return terrainLine != null && terrainLine.HasSections();
	}

	private void DrawGizmos() {
		for (int i = 0; i < terrainLine.sections.Count; i++) {
			if (i % 2 == 1) SetGizmoColorType1();
			else SetGizmoColorType2();
			UpdateGizmoColor();
			DrawGizmosForSection(terrainLine.sections[i]);
		}
	}

	private void DrawGizmosForSection(TerrainLineSection section) {
		DrawGizmoForPoint(section.startPoint);
		foreach (Vector2 point in section.midPoints) {
			DrawGizmoForPoint(point);
		}
	}

	private void DrawGizmoForPoint(Vector2 point) {
		float radius = 0.5f;
		Vector3 point3D = new Vector3(point.x, point.y, transform.position.z);
		Gizmos.DrawSphere(point3D, radius);
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
