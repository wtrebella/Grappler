using UnityEngine;
using System.Collections;

public class Rect3D {
	public Vector3 origin;
	public Vector3 size;

	public Rect3D() {
		origin = Vector3.zero;
		size = Vector3.zero;
	}

	public Rect3D(Vector3 origin, Vector3 size) {
		this.origin = origin;
		this.size = size;
	}

	public void Log() {
		Debug.Log("origin: " + origin + ", size: " + size);
	}
}
