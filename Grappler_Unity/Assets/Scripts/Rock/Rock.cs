using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(RockMeshCreator))]
public class Rock : MonoBehaviour {
	[SerializeField] private int numPoints = 30;
	[SerializeField] private float radius = 2;
	
	private PolygonCollider2D polygonCollider;
	private RockMeshCreator meshCreator;
	
	public void Generate() {
		List<Vector2> points = new List<Vector2>();
		GeneratePoints(points);
		SortPoints(points);
		Vector2[] pointsArray = points.ToArray();
		polygonCollider.points = pointsArray;
		meshCreator.InitMesh(pointsArray);
	}

	private void Awake () {
		polygonCollider = GetComponent<PolygonCollider2D>();
		meshCreator = GetComponent<RockMeshCreator>();
	}

	private void Start() {
		Generate();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.G)) Generate();
	}

	private void GeneratePoints(List<Vector2> points) {
		for (int i = 0; i < numPoints; i++) {
			float x = (Random.value - 0.5f) * radius;
			float y = (Random.value - 0.5f) * radius;
			Vector2 point = new Vector2(x, y);
			points.Add(point);
		}
	}

	private void SortPoints(List<Vector2> points) {
		points.SortWithTwoPeasantsPolygonAlgorithm();
	}
}
