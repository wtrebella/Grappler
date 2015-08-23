using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class AreaCircle : MonoBehaviour {
	[SerializeField] private int resolution = 10;
	[SerializeField] private float radius = 5;
	[SerializeField] private float segmentHeight = 2;
	[SerializeField] private Color color = Color.white;

	private MeshFilter meshFilter;

	private void Awake() {
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();

		DrawMesh();
	}

	private void DrawMesh() {
		Vector3[] verts = new Vector3[4 * (resolution / 2)];
		Color[] colors = new Color[4 * (resolution / 2)];
		Vector2[] uvs = new Vector2[4 * (resolution / 2)];
		int[] tris = new int[6 * (resolution / 2)];
		int vertIndex = 0;
		int triIndex = 0;
		float arcAngle = 360.0f / resolution;

		for (int i = 0; i < resolution; i += 2) {
			Vector2 vert0 = Vector2.zero;
			Vector2 vert1 = Vector2.zero;
			Vector2 vert2 = Vector2.zero;
			Vector2 vert3 = Vector2.zero;

			float angle0 = arcAngle * i * Mathf.Deg2Rad;
			float angle1 = arcAngle * (i+1) * Mathf.Deg2Rad;
			float shortRadius = radius - segmentHeight;

			vert0.x = Mathf.Cos(angle0) * shortRadius;
			vert0.y = Mathf.Sin(angle0) * shortRadius;

			vert1.x = Mathf.Cos(angle0) * radius;
			vert1.y = Mathf.Sin(angle0) * radius;

			vert2.x = Mathf.Cos(angle1) * radius;
			vert2.y = Mathf.Sin(angle1) * radius;

			vert3.x = Mathf.Cos(angle1) * shortRadius;
			vert3.y = Mathf.Sin(angle1) * shortRadius;

			verts[0 + vertIndex] = vert0;
			verts[1 + vertIndex] = vert1;
			verts[2 + vertIndex] = vert2;
			verts[3 + vertIndex] = vert3;

			colors[0 + vertIndex] = color;
			colors[1 + vertIndex] = color;
			colors[2 + vertIndex] = color;
			colors[3 + vertIndex] = color;

			uvs[0 + vertIndex] = new Vector2(0, 0);
			uvs[1 + vertIndex] = new Vector2(1, 0);
			uvs[2 + vertIndex] = new Vector2(1, 1);
			uvs[3 + vertIndex] = new Vector2(1, 0);

			tris[0 + triIndex] = 0 + vertIndex;
			tris[1 + triIndex] = 1 + vertIndex;
			tris[2 + triIndex] = 2 + vertIndex;

			tris[3 + triIndex] = 2 + vertIndex;
			tris[4 + triIndex] = 3 + vertIndex;
			tris[5 + triIndex] = 0 + vertIndex;

			vertIndex += 4;
			triIndex += 6;
		}

		meshFilter.mesh.vertices = verts;
		meshFilter.mesh.triangles = tris;
		meshFilter.mesh.uv = uvs;
		meshFilter.mesh.colors = colors;

		meshFilter.mesh.RecalculateBounds();
		meshFilter.mesh.RecalculateNormals();
	}
}
