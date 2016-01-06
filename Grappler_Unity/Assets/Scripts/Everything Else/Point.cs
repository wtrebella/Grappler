using UnityEngine;
using System.Collections;

public class Point {
	public Vector2 pointVector;

	public Point() {
		pointVector = Vector2.zero;
	}

	public Point(float x, float y) {
		pointVector = new Vector2(x, y);
	}

	public Point(Vector2 point) {
		this.pointVector = point;
	}
}
