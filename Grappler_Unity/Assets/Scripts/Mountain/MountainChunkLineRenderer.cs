using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunkLineRenderer : MonoBehaviour {
	[SerializeField] private float startWidth = 6;
	[SerializeField] private float endWidth = 6;

	private LineRenderer lineRenderer;
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;

	private void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
		meshCreator.SignalMountainCreated += HandleMountainCreated;
		lineRenderer.SetWidth(startWidth, endWidth);
	}

	private void HandleMountainCreated() {
		DrawLine();
	}

	private void DrawLine() {
		int oneDirectionCount = polygonCollider.points.Length - 1;
		int index = 0;
		lineRenderer.SetVertexCount(oneDirectionCount * 2);
		Vector3 firstPoint = polygonCollider.points[oneDirectionCount];
		firstPoint.z = 0.1f;
		lineRenderer.SetPosition(0, firstPoint);
		index++;
		for (int i = 0; i < polygonCollider.points.Length - 2; i++) {
			Vector3 point = polygonCollider.points[i];
			point.z = 0.1f;
			lineRenderer.SetPosition(index, point);
			index++;
		}
		for (int i = polygonCollider.points.Length - 3; i >= 0; i--) {
			Vector3 point = polygonCollider.points[i];
			point.z = 0.1f;
			lineRenderer.SetPosition(index, point);
			index++;
		}

		Vector3 lastPoint = polygonCollider.points[oneDirectionCount];
		lastPoint.z = 0.1f;
		lineRenderer.SetPosition(index, lastPoint);
	}
}
