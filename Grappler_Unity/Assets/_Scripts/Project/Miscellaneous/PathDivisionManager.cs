using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public class PathDivisionManager : MonoBehaviour {
	public Action<PathDivision> SignalPathDivisionCreated;

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

	private void OnTopContourChanged() {
		if (path.topContour.sections.Count == 0) return;
		xRangeTop.min = path.topContour.GetWorldStartPoint().x;
		xRangeTop.max = path.topContour.GetWorldEndPoint().x;
		UpdateXRange();
	}

	private void OnBottomContourChanged() {
		if (path.bottomContour.sections.Count == 0) return;
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
		foreach (PathDivision division in divisionsToRemove) {
			divisions.Remove(division);
			GameObject.Destroy(division.gameObject);
		}
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
			if (SignalPathDivisionCreated != null) SignalPathDivisionCreated(division);
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
		PathDivision division = new GameObject("", typeof(PathDivision)).GetComponent<PathDivision>();
		division.transform.SetParent(transform);
		division.Initialize(path, xStart, xStart + divisionSize);
		division.name = "Path Division (" + ((int)xStart).ToString() + ")";
		division.transform.position = path.bottomContour.GetPointAtX(division.xRange.min);
		return division;
	}

	private float GetLastDivisionX() {
		if (divisions.Count == 0) return 0;
		return divisions.GetLast().xRange.max;
	}
}