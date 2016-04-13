using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinPatternData {
	public List<Vector2> points {get; private set;}

	public CoinPatternData() {
		points = new List<Vector2>();
	}

	public void AddPoint(Vector2 point) {
		points.Add(point);
	}
}
