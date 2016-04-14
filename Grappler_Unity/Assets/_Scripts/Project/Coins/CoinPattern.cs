using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinPattern {
	public List<Vector2> points {get; private set;}

	public CoinPattern() {
		points = new List<Vector2>();
	}

	public void AddPoint(Vector2 point) {
		points.Add(point);
	}
}
