using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(MountainChunkMeshCreator))]
public class MountainChunkVectorLine : MonoBehaviour {
	[SerializeField] private float width = 30;
	[SerializeField] private Camera gameCamera;

	private VectorLine line;
	private PolygonCollider2D polygonCollider;
	private MountainChunkMeshCreator meshCreator;

	private void Awake() {
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<MountainChunkMeshCreator>();
		meshCreator.SignalMountainCreated += HandleMountainCreated;
	}

	private void InitLine() {
		line = new VectorLine("Mountain Chunk Line", GetListOfLinePoints(), width, LineType.Continuous, Joins.Weld);
		line.SetColor(new Color32(1, 1, 1, 1));
	}

	private void HandleMountainCreated() {
		DrawLine();
	}

	private List<Vector3> GetListOfLinePoints() {
		List<Vector3> list = new List<Vector3>();
		for (int i = 0; i < polygonCollider.points.Length - 3; i++) list.Add(polygonCollider.points[i]);
		return list;
	}

	private void DrawLine() {
		if (line == null) InitLine();
		else line.points3 = GetListOfLinePoints();
		line.Draw3D();
	}
}
