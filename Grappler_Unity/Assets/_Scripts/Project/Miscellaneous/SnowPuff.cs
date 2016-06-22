using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SnowPuff : MonoBehaviour {
	[SerializeField] private tk2dSlicedSprite sprite;
	[SerializeField] private float pixelsPerUnit = 32;
	[SerializeField] private float pixelRadius = 80;

	private float PixelsToUnits(float pixels) {
		return pixels / pixelsPerUnit;
	}

	private float UnitsToPixels(float units) {
		return units * pixelsPerUnit;
	}

	private void SetPoints(Vector2 pointA, Vector2 pointB) {
		// get length here and set it
		// figure out the rotations and how to line it up with the point slope
	}

	private void SetLength(float unitLength) {
		float pixelLength = UnitsToPixels(unitLength) + 2 * pixelRadius;
		sprite.dimensions = new Vector2(pixelLength, sprite.dimensions.y);
	}




	private void Awake() {
		SetLength(10);
	}
	
	private void Start() {
	
	}
	
	private void Update() {
	
	}
}
