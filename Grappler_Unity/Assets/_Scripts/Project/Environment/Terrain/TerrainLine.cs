using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TerrainLine : MonoBehaviour {
	public List<Point> edgePoints {get; private set;}

	private Vector2 slopeVector;
	private float slopeVal;

//	public void AddSegment

	private void Awake() {
		edgePoints = new List<Point>();
	}

	private void Reset() {
		edgePoints.Clear();
		slopeVector = Vector2.zero;
	}


}