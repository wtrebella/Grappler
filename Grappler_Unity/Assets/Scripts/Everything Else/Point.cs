using UnityEngine;
using System.Collections;

public class Point {
	public Vector2 point;

	public Point() {
		point = Vector2.zero;
	}

	public Point(float x, float y) {
		point = new Vector2(x, y);
	}

	public Point(Vector2 point) {
		this.point = point;
	}
}
