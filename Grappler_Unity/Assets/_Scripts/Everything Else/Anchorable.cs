using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Anchorable : MonoBehaviour {
	public Point linePoint {get; private set;}
	public int anchorableID {get; private set;}

	public bool isConnected {get; private set;}
	public Rigidbody2D rigid {get; private set;}
	private AnchorPoint anchorPoint;

	public void SetAnchorableID(int id) {
		anchorableID = id;
	}

	public void SetLinePoint(Point point) {
		linePoint = point;
	}

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		anchorPoint = GetComponentInChildren<AnchorPoint>();

		WhitTools.Assert(anchorPoint != null, "object has no anchor point!");
	}

	public void HandleRelease() {
		isConnected = false;
	}

	public void HandleConnected() {
		isConnected = true;
	}

	public Vector2 GetAnchorPoint() {
		return anchorPoint.transform.localPosition;
	}
}
