using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PolydrawPoint3 {
	public Vector3 vector;

	public bool borderIgnored = false;

	public float x {
		get {return vector.x;}
		set {vector.x = value;}
	}

	public float y {
		get {return vector.y;}
		set {vector.y = value;}
	}

	public float z {
		get {return vector.z;}
		set {vector.z = value;}
	}

	public PolydrawPoint3() {
		vector = Vector3.zero;
	}

	public PolydrawPoint3(float x, float y, float z) {
		vector = new Vector3(x, y, z);
	}

	public PolydrawPoint3(Vector3 point) {
		this.vector = point;
	}
}
