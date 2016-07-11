using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public class PathDivision : MonoBehaviour {
	public static bool debugDivisions = true;

	public Path path {get; private set;}
	public FloatRange xRange {get; private set;}
	public FloatRange distRangeTop {get; private set;}
	public FloatRange distRangeBottom {get; private set;}

	private List<object> objects;
	private bool initialized = false;

	public void Initialize(Path path, float min, float max) {
		this.path = path;
		objects = new List<object>();
		xRange = new FloatRange(min, max);
		CalculateDists();
		initialized = true;
	}

	private void CalculateDists() {
		distRangeBottom = new FloatRange();
		distRangeBottom.min = path.bottomContour.GetDistAtX(xRange.min);
		distRangeBottom.max = path.bottomContour.GetDistAtX(xRange.max);

		distRangeTop = new FloatRange();
		distRangeTop.min = path.topContour.GetDistAtX(xRange.min);
		distRangeTop.max = path.topContour.GetDistAtX(xRange.max);
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