using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Range {
	public Range() {

	}
}

[Serializable]
public class IntRange : Range {
	public int min = 0;
	public int max = 1;
	
	public IntRange() {
		
	}
	
	public IntRange(int min, int max) {
		this.min = min;
		this.max = max;
	}

	public int GetRandom() {
		return UnityEngine.Random.Range(min, max);
	}
}

[Serializable]
public class FloatRange : Range {
	public float min = 0;
	public float max = 1;

	public FloatRange() {

	}

	public FloatRange(float min, float max) {
		this.min = min;
		this.max = max;
	}

	public float GetRandom() {
		return UnityEngine.Random.Range(min, max);
	}
}

[Serializable]
public class Vector2Range : Range {
	public Vector2 min = new Vector2(0, 0);
	public Vector2 max = new Vector2(1, 1);
	
	public Vector2Range() {
		
	}
	
	public Vector2Range(Vector2 min, Vector2 max) {
		this.min = min;
		this.max = max;
	}

	public Vector2 GetRandom() {
		float x = UnityEngine.Random.Range(this.min.x, this.max.x);
		float y = UnityEngine.Random.Range(this.min.y, this.max.y);
		Vector2 point = new Vector2(x, y);
		return point;
	}
}

[Serializable]
public class Vector3Range : Range {
	public Vector3 min = new Vector3(0, 0, 0);
	public Vector3 max = new Vector3(1, 1, 1);
	
	public Vector3Range() {
		
	}
	
	public Vector3Range(Vector3 min, Vector3 max) {
		this.min = min;
		this.max = max;
	}

	public Vector3 GetRandom() {
		float x = UnityEngine.Random.Range(this.min.x, this.max.x);
		float y = UnityEngine.Random.Range(this.min.y, this.max.y);
		float z = UnityEngine.Random.Range(this.min.z, this.max.z);
		Vector3 point = new Vector3(x, y, z);
		return point;
	}
}
