using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	[RequireComponent(typeof(TriangulatedMesh))]
	[RequireComponent(typeof(PolygonCollider2D))]
	public class ContourMesh : MonoBehaviour {
		public enum WhitTerrainMeshType {
			None,
			BottomEdge,
			TopEdge
		}

		[SerializeField] private WhitTerrainMeshType meshType;
		[SerializeField] private Contour terrain;
		[SerializeField] private Vector2 outlineSize = new Vector2(1, 200);

		private bool isDirty = false;

		private PolygonCollider2D polygonCollider;
		private TriangulatedMesh mesh;

		public void Redraw() {
			isDirty = false;

			List<Vector2> points = terrain.GetPointsLocal();
			Vector2 startPointLocal = terrain.GetLocalStartPoint();
			Vector2 endPointLocal = terrain.GetLocalEndPoint();

			float yMin = Mathf.Min(startPointLocal.y, endPointLocal.y);
			float yMax = Mathf.Max(startPointLocal.y, endPointLocal.y);
			float yExtant = 0;
			if (meshType == WhitTerrainMeshType.BottomEdge) yExtant = yMax;
			else if (meshType == WhitTerrainMeshType.TopEdge) yExtant = yMin;

			Vector2 p1 = endPointLocal + Vector2.right * outlineSize.x;
			Vector2 p2 = new Vector2(p1.x, yExtant + (GetMeshDirection() * outlineSize.y).y);
			Vector2 p3 = new Vector2(startPointLocal.x - outlineSize.x, p2.y);
			Vector2 p4 = new Vector2(p3.x, startPointLocal.y);

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
			polygonCollider = GetComponent<PolygonCollider2D>();

			terrain.SignalTerrainChanged += OnTerrainChanged;
		}

		private void OnTerrainChanged() {
			isDirty = true;
		}

		private void LateUpdate() {
			if (isDirty) Redraw();
		}
	}
}