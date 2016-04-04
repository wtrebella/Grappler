using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(TriangulatedMesh))]
public class WhitTerrainMesh : MonoBehaviour {
	public enum WhitTerrainMeshType {
		None,
		BottomEdge,
		TopEdge
	}

	[SerializeField] private WhitTerrainMeshType meshType;
	[SerializeField] private WhitTerrainLine terrainLine;
	[SerializeField] private PolygonCollider2D polygonCollider;

	private bool isDirty = false;

	private TriangulatedMesh mesh;

	public void SetDirty() {
		isDirty = true;
	}

	public void Redraw() {
		isDirty = false;

		List<Vector2> points = terrainLine.GetPoints();
		Vector2 firstPoint = terrainLine.GetFirstPointLocal();
		Vector2 lastPoint = terrainLine.GetLastPointLocal();

		float amt = 100;

		Vector2 p1 = lastPoint + Vector2.right * amt;
		Vector2 p2 = p1 + GetMeshDirection() * amt;
		Vector2 p3 = new Vector2(firstPoint.x - amt, p2.y);
		Vector2 p4 = new Vector2(p3.x, firstPoint.y);

		points.Add(p1);
		points.Add(p2);
		points.Add(p3);
		points.Add(p4);

		var pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		mesh.RedrawMesh(pointsArray);
	}

	private Vector2 GetMeshDirection() {
		if (meshType == WhitTerrainMeshType.BottomEdge) return Vector2.up;
		else if (meshType == WhitTerrainMeshType.TopEdge) return Vector2.down;
		else {
			Debug.LogError("invalid mesh type: " + meshType.ToString());
			return Vector2.zero;
		}
	}

	private void Awake() {
		mesh = GetComponent<TriangulatedMesh>();
	}

	private void LateUpdate() {
		if (isDirty) Redraw();
	}
}