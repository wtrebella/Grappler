using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class WhitTerrainSection {
	public Vector2 startPoint {get {return config.startPoint;}}
	public float length {get {return config.length;}}
	public float slope {get {return config.slope;}}
	public float distStart {get {return config.distStart;}}

	public Vector2 endPoint {get; private set;}
	public List<Vector2> midPoints {get; private set;}
	public List<Vector2> allPoints {get; private set;}
	public float surfaceLength {get; private set;}
	public float distEnd {get; private set;}

	private WhitTerrain terrain;
	private Dictionary<int, float> surfaceDists;
	private WhitTerrainSectionConfig config;
	private WhitTerrainSectionAttributes attributes;
	private Vector2 slopeVector;
	private Vector2 perpendicularSlopeVector;

	public WhitTerrainSection(WhitTerrain terrain, WhitTerrainSectionConfig config, WhitTerrainSectionAttributes attributes) {
		this.terrain = terrain;
		this.config = config;
		this.attributes = attributes;

		CalculateSlopeVector();
		CalculatePerpendicularSlopeVector();
		GenerateEndPoint();
		GenerateMidPoints();
		BumpifyMidPoints();
		CollectAllPointsInList();
		CalculateSurfaceMeasures();
		CalculateDistAtEnd();
	}

	public Vector2 GetDirection() {
		return (endPoint - startPoint).normalized;
	}

	public Vector2 GetPointAtDist(float dist) {
		float percent = DistToPercent(dist);
		return startPoint + GetDirection() * length * percent;
	}

	public Vector2 GetSurfacePointAtDist(float dist) {
		float percent = DistToPercent(dist);
		Vector2 localPoint = GetLocalSurfacePointAtPercent(percent);
		Vector2 worldPoint = terrain.transform.TransformPoint(localPoint);
		return worldPoint;
	}

	private Vector2 GetLocalSurfacePointAtPercent(float percent) {
		percent = Mathf.Clamp01(percent);
		float targetSurfaceDist = PercentToSurfaceDist(percent);

		Vector2 pointA = Vector2.zero;
		Vector2 pointB = Vector2.zero;
		float surfaceDistA = 0;
		float surfaceDistB = 0;

		for (int i = 0; i < allPoints.Count - 1; i++) {
			pointA = allPoints[i];
			pointB = allPoints[i+1];
			surfaceDistA = surfaceDists[i];
			surfaceDistB = surfaceDists[i+1];
			if (targetSurfaceDist >= surfaceDistA && targetSurfaceDist < surfaceDistB) break;
		}

		float targetSegmentDist = targetSurfaceDist - surfaceDistA;
		Vector2 segmentDirection = (pointB - pointA).normalized;
		Vector2 position = pointA + segmentDirection * targetSegmentDist;
		return position;
	}

	private float DistToPercent(float dist) {
		return dist - distStart;
	}

	private float PercentToSurfaceDist(float percent) {
		return percent * surfaceLength;
	}

	private void CalculateDistAtEnd() {
		distEnd = distStart + length;
	}

	private void CalculateSlopeVector() {
		slopeVector = WhitTools.SlopeToDirection(slope);
	}

	private void CalculatePerpendicularSlopeVector() {
		perpendicularSlopeVector = new Vector2(slopeVector.y, -slopeVector.x);
	}

	private void CalculateSurfaceMeasures() {
		surfaceDists = new Dictionary<int, float>();
		surfaceDists.Add(0, 0.0f);

		Vector2 prevPoint = startPoint;
		float surfaceDist = 0;

		for (int i = 1; i < allPoints.Count; i++) {
			Vector2 point = allPoints[i];
			surfaceDist += (point - prevPoint).magnitude;
			surfaceDists.Add(i, surfaceDist);
			prevPoint = point;
		}

		surfaceLength = surfaceDists[allPoints.Count - 1];
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

	private void CollectAllPointsInList() {
		allPoints = new List<Vector2>();
		allPoints.Add(startPoint);
		allPoints.AddAll(midPoints);
		allPoints.Add(endPoint);
	}
}