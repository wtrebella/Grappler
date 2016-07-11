using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public enum PathDivisionPositionType {
	Top,
	Bottom,
	Middle
}

public class PathDivision : MonoBehaviour {
	public static bool debugDivisions = true;

	public Path path {get; private set;}
	public FloatRange xRange {get; private set;}
	public FloatRange distRangeTop {get; private set;}
	public FloatRange distRangeBottom {get; private set;}
	public Vector2 bottomLeft {get; private set;}
	public Vector2 bottomRight {get; private set;}
	public Vector2 topLeft {get; private set;}
	public Vector2 topRight {get; private set;}

	private List<object> objects;
	private bool initialized = false;

	public void Initialize(Path path, float min, float max) {
		this.path = path;
		objects = new List<object>();
		xRange = new FloatRange(min, max);
		CalculateDists();
		CalculatePoints();
		initialized = true;
	}

	public void AddObject(GameObject prefab, PathDivisionPositionType positionType) {
		if (positionType == PathDivisionPositionType.Top) AddObject(prefab, topLeft);
		else if (positionType == PathDivisionPositionType.Bottom) AddObject(prefab, bottomLeft);
		else if (positionType == PathDivisionPositionType.Middle) AddObject(prefab, WhitTools.GetAveragePoint(topLeft, bottomLeft));
	}

	public void AddObject(GameObject prefab, Vector2 position) {
		GameObject obj = Instantiate(prefab);
		obj.transform.SetParent(transform);
		obj.transform.position = position;
	}

	private void CalculateDists() {
		distRangeBottom = new FloatRange();
		distRangeBottom.min = path.bottomContour.GetDistAtX(xRange.min);
		distRangeBottom.max = path.bottomContour.GetDistAtX(xRange.max);

		distRangeTop = new FloatRange();
		distRangeTop.min = path.topContour.GetDistAtX(xRange.min);
		distRangeTop.max = path.topContour.GetDistAtX(xRange.max);
	}

	private void CalculatePoints() {
		bottomLeft = path.bottomContour.GetPointAtX(xRange.min);
		bottomRight = path.bottomContour.GetPointAtX(xRange.max);
		topLeft = path.topContour.GetPointAtX(xRange.min);
		topRight = path.topContour.GetPointAtX(xRange.max);
	}

	private void OnDrawGizmos() {
		if (ShouldDrawDebug()) ShowDivisionDebugLines();
	}

	private bool ShouldDrawDebug() {
		return debugDivisions && initialized;
	}

	private void ShowDivisionDebugLines() {
		float size = 1000;
		Gizmos.DrawLine(new Vector3(xRange.min, -size, 0), new Vector3(xRange.min, size, 0));
		Gizmos.DrawLine(new Vector3(xRange.max, -size, 0), new Vector3(xRange.max, size, 0));
	}
}