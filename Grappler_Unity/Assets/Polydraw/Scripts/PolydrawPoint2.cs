using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PolydrawPoint2 {
	public Vector2 vector;

	public bool borderIgnored = false;

	public float x {
		get {return vector.x;}
		set {vector.x = value;}
	}
	public float y {
		get {return vector.y;}
		set {vector.y = value;}
	}

	public PolydrawPoint2() {
		vector = Vector2.zero;
	}

	public PolydrawPoint2(float x, float y) {
		vector = new Vector2(x, y);
	}

	public PolydrawPoint2(Vector2 point) {
		this.vector = point;
	}
}