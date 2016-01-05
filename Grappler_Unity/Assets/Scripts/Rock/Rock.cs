using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(RockMeshCreator))]
public class Rock : MonoBehaviour {
	[SerializeField] private int numPoints = 30;
	
	private PolygonCollider2D polygonCollider;
	private RockMeshCreator meshCreator;
	
	public void Generate() {
		List<Vector2> points = new List<Vector2>();
		GeneratePoints(ref points);
		SortPoints(ref points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
//		meshCreator.InitMesh(pointsArray);
	}
	
	private void Awake () {
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<RockMeshCreator>();
	}

	private void GeneratePoints(ref List<Vector2> points) {
		for (int i = 0; i < numPoints; i++) {
			Vector2 point = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
			points.Add(point);
		}
	}

	private void SortPoints(ref List<Vector2> points) {
		points.SortWithTwoPeasantsPolygonAlgorithm();
	}
}
