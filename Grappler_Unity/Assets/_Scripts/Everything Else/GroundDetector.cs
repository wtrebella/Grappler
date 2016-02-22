﻿using UnityEngine;
using System.Collections;

public class GroundDetector : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float maxDistance = 0.5f;
	[SerializeField] private FloatRange distanceFromGroundRange = new FloatRange(0.5f, 5);

	public bool IsCloseToGround() {
		return IsWithinDistanceToGround(maxDistance);
	}

	public float GetDistanceFromGround() {
		RaycastHit2D hit = Physics2D.Raycast(
			rigid.transform.position, 
			new Vector2(0, -1).normalized, 
			Mathf.Infinity, 
			1 << LayerMask.NameToLayer("Ground"));

		Vector2 position = rigid.transform.position.ToVector2();
		if (hit.collider) return (position - hit.point).magnitude;
		else {
			Debug.LogError("no ground found!");
			return 10;
		}
	}

	public float GetDistanceFromGroundPercent() {
		float distance = GetDistanceFromGround();
		return distanceFromGroundRange.GetPercent(distance);
	}

	public bool IsWithinDistanceToGround(float distance) {
		RaycastHit2D hit = Physics2D.Raycast(
			rigid.transform.position, 
			new Vector2(1, -1).normalized, 
			distance, 
			1 << LayerMask.NameToLayer("Ground"));

		return hit.collider != null;
	}
}
