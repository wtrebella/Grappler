using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class MountainChunkVectorLine : MonoBehaviour {
	[SerializeField] private float width = 20;

	private VectorLine line;

	public void AddToLine(MountainChunk mountainChunk) {
		if (line == null) InitLine();

		List<Vector2> points = mountainChunk.GetListOfLinePoints();
		if (line.points3.Count > 0) points.RemoveAt(0);
		foreach (Vector2 point in points) line.points3.Add(new Vector3(point.x, point.y, -0.1f));
		line.endPointsUpdate = points.Count;

		line.Draw3DAuto();
	}

	private void InitLine() {
		VectorManager.useDraw3D = true;
		line = new VectorLine("Mountain Chunk Line", new List<Vector3>(), width, LineType.Continuous, Joins.Weld);
		line.SetColor(new Color32(1, 1, 1, 1));
	}
}
