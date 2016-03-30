using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainLineSection {
	public Vector2 startPoint {get; private set;}
	public Vector2 endPoint {get; private set;}
	public List<Vector2> midPoints {get; private set;}
	public float slope {get; private set;}

	private TerrainLineAttributes attributes;
	private Vector2 slopeVector;
	private Vector2 perpendicularSlopeVector;

	public TerrainLineSection(Vector2 startPoint, float slope, TerrainLineAttributes attributes) {
		this.attributes = attributes;
		this.slope = slope;
		this.startPoint = startPoint;

		CalculateSlopeVector();
		CalculatePerpendicularSlopeVector();
		GenerateMidPoints();
		BumpifyMidPoints();
		GenerateEndPoint();
	}

	private void CalculateSlopeVector() {
		slopeVector = WhitTools.SlopeToVector2(slope);
	}

	private void CalculatePerpendicularSlopeVector() {
		perpendicularSlopeVector = new Vector2(slopeVector.y, -slopeVector.x);
	}

	private void GenerateMidPoints() {
		midPoints = new List<Vector2>();

		float distance = 0;
		Vector2 prevPoint = startPoint;

		while (distance < attributes.sectionLength) {
			float segmentLength = attributes.sectionSegmentLengthRange.GetRandom();
			Vector2 nextPoint = prevPoint + slopeVector * segmentLength;
			distance += segmentLength;
			midPoints.Add(nextPoint);
		}
	}

	private void GenerateEndPoint() {
		endPoint = startPoint + slopeVector * attributes.sectionLength;
	}

	private void BumpifyMidPoints() {
		for (int i = 0; i < midPoints.Count; i++) {
			Vector2 point = midPoints[i];
			float bumpHeight = UnityEngine.Random.Range(-attributes.maxBumpHeight, attributes.maxBumpHeight);
			point += perpendicularSlopeVector * bumpHeight;
		}
	}
}