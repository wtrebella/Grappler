using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public class PathDivision {
	public object slot;
	public FloatRange xRange = new FloatRange(0, 0);

	public PathDivision() {

	}
}

public class ItemGenerator : MonoBehaviour {
	[SerializeField] private bool debugDivisions = false;
	[SerializeField] private Path path;
	[SerializeField] private float divisionSize = 10;

	private FloatRange xRangeTop = new FloatRange(0, 0);
	private FloatRange xRangeBottom = new FloatRange(0, 0);
	private FloatRange xRange = new FloatRange(0, 0);

	private List<PathDivision> divisions;

	private void Awake() {
		divisions = new List<PathDivision>();
		path.topContour.SignalContourChanged += OnTopContourChanged;
		path.bottomContour.SignalContourChanged += OnBottomContourChanged;
	}
	
	private void Start() {
	
	}
	
	private void OnDrawGizmos() {
		if (debugDivisions) ShowDivisionDebugLines();
	}

	private void ShowDivisionDebugLines() {
		if (divisions == null) return;

		float size = 1000;
		foreach (PathDivision division in divisions) {
			Gizmos.DrawLine(new Vector3(division.xRange.min, -size, 0), new Vector3(division.xRange.min, size, 0));
		}
		PathDivision lastDivision = divisions.GetLast();
		Gizmos.DrawLine(new Vector3(lastDivision.xRange.max, -size, 0), new Vector3(lastDivision.xRange.max, size, 0));
	}

	private void OnTopContourChanged() {
		xRangeTop.min = path.topContour.GetWorldStartPoint().x;
		xRangeTop.max = path.topContour.GetWorldEndPoint().x;
		UpdateXRange();
	}

	private void OnBottomContourChanged() {
		xRangeBottom.min = path.bottomContour.GetWorldStartPoint().x;
		xRangeBottom.max = path.bottomContour.GetWorldEndPoint().x;
		UpdateXRange();
	}

	private void UpdateXRange() {
		xRange.min = Mathf.Max(xRangeTop.min, xRangeBottom.min);
		xRange.max = Mathf.Min(xRangeTop.max, xRangeBottom.max);
		RemoveDivisions();
		CreateDivisions();
	}

	private void RemoveDivisions() {
		var divisionsToRemove = GetDivisionsBehindMinX();
		foreach (PathDivision division in divisionsToRemove) divisions.Remove(division);
	}

	private void CreateDivisions() {
		float lastDivisionX = GetLastDivisionX();
		float totalWidth = xRange.max - lastDivisionX;
		if (totalWidth < divisionSize) return;

		int numDivisions = (int)(totalWidth / divisionSize);

		for (int i = 0; i < numDivisions; i++) {
			float xStart = lastDivisionX + i * divisionSize;
			PathDivision division = CreateDivision(xStart);
			divisions.Add(division);
		}
	}

	private List<PathDivision> GetDivisionsBehindMinX() {
		float xMin = xRange.min;
		List<PathDivision> divisionsBehindMinX = new List<PathDivision>();
		foreach (PathDivision division in divisions) {
			if (division.xRange.min < xMin) divisionsBehindMinX.Add(division);
		}
		return divisionsBehindMinX;
	}

	private PathDivision CreateDivision(float xStart) {
		PathDivision division = new PathDivision();
		division.xRange = new FloatRange(xStart, xStart + divisionSize);
		return division;
	}

	private float GetLastDivisionX() {
		if (divisions.Count == 0) return 0;
		return divisions.GetLast().xRange.max;
	}
}