using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace WhitTerrain {
	public class ContourLinePiece : MonoBehaviour {
		[SerializeField] private tk2dSlicedSprite sprite;
		[SerializeField] private float pixelsPerUnit = 32;
		[SerializeField] private float pixelRadius = 80;

		private float PixelsToUnits(float pixels) {
			return pixels / pixelsPerUnit;
		}

		private float UnitsToPixels(float units) {
			return units * pixelsPerUnit;
		}

		public void SetPoints(Vector2 pointA, Vector2 pointB) {
			Vector2 pointVector = pointB - pointA;
			Vector2 pointDirection = pointVector.normalized;
			float length = pointVector.magnitude;
			float angle = WhitTools.DirectionToAngle(pointDirection);
			SetLength(length);
			SetLocalRotation(angle);
			SetLocalPosition(pointA);
		}

		private void SetLength(float unitLength) {
			float pixelLength = UnitsToPixels(unitLength) + 2 * pixelRadius;
			sprite.dimensions = new Vector2(pixelLength, sprite.dimensions.y);
		}

		private void SetLocalRotation(float angle) {
			Vector3 eulers = transform.localEulerAngles;
			eulers.z = angle;
			transform.localEulerAngles = eulers;
		}

		private void SetLocalPosition(Vector2 localPosition) {
			transform.localPosition = localPosition;
		}

		private void Awake() {

		}
		
		private void Start() {
		
		}
		
		private void Update() {
		
		}
	}
}