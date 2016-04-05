using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class WhitTerrainSection {
	public Vector2 startPoint {get {return config.startPoint;}}
	public float length {get {return config.length;}}
	public float slope {get {return config.slope;}}

	public Vector2 endPoint {get; private set;}
	public List<Vector2> midPoints {get; private set;}

	private WhitTerrainSectionConfig config;
	private WhitTerrainSectionAttributes attributes;
	private Vector2 slopeVector;
	private Vector2 perpendicularSlopeVector;

	public WhitTerrainSection(WhitTerrainSectionConfig config, WhitTerrainSectionAttributes attributes) {
		this.config = config;
		this.attributes = attributes;

		CalculateSlopeVector();
		CalculatePerpendicularSlopeVector();
		GenerateEndPoint();
		GenerateMidPoints();
		BumpifyMidPoints();
	}

	public Vector2 GetDirection() {
		return (endPoint - startPoint).normalized;
	}

	private void CalculateSlopeVector() {
		slopeVector = WhitTools.SlopeToDirection(slope);
	}

	private void CalculatePerpendicularSlopeVector() {
		perpendicularSlopeVector = new Vector2(slopeVector.y, -slopeVector.x);
	}

	private void GenerateMidPoints() {
		midPoints = new List<Vector2>();

		Vector2 prevPoint = startPoint;
		float distance = 0;
		bool isOnLastPoint = false;

		while (!isOnLastPoint) {
			float segmentLength = attributes.sectionSegmentLengthRange.GetRandom();
			distance += segmentLength;
			float sqrSectionSegmentLength = Mathf.Pow(segmentLength, 2);
			Vector2 nextPoint = prevPoint + slopeVector * segmentLength;
			Vector2 previousPointToEndPointVector = endPoint - prevPoint;
			Vector2 nextPointToEndPointVector = endPoint - nextPoint;
			float nextPointToEndPointSqrMagnitude = nextPointToEndPointVector.sqrMagnitude;

			isOnLastPoint = 
				(nextPointToEndPointSqrMagnitude < sqrSectionSegmentLength) ||
				distance > length;

			if (isOnLastPoint) nextPoint = prevPoint + previousPointToEndPointVector / 2f;

			midPoints.Add(nextPoint);
			prevPoint = nextPoint;
		}
	}

	private void GenerateEndPoint() {
		endPoint = startPoint + slopeVector * length;
	}

	private void BumpifyMidPoints() {
		for (int i = 0; i < midPoints.Count; i++) {
			Vector2 point = midPoints[i];
			float bumpHeight = UnityEngine.Random.Range(-attributes.maxBumpHeight, attributes.maxBumpHeight);
			point += perpendicularSlopeVector * bumpHeight;
			midPoints[i] = point;
		}
	}
}