using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TerrainLine))]
public class TerrainLineDebug : MonoBehaviour {
	public bool isOn = true;
	[Range(0.1f, 0.9f)] public float sphereRadius = 0.5f;

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
			DrawGizmosForSection(terrainLine.sections[i], i == terrainLine.sections.Count - 1);
		}
	}

	private void DrawGizmosForSection(TerrainLineSection section, bool withEndPoint) {
		DrawGizmoForPoint(section.startPoint);
		foreach (Vector2 point in section.midPoints) DrawGizmoForPoint(point);
		if (withEndPoint) DrawGizmoForPoint(section.endPoint);
	}

	private void DrawGizmoForPoint(Vector2 point) {
		Vector3 point3D = new Vector3(point.x, point.y, transform.position.z);
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
