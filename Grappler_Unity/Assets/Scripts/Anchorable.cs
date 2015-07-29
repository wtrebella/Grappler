using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Anchorable : MonoBehaviour {
	public bool isConnected {get; private set;}
	public Rigidbody2D rigid {get; private set;}
	private AnchorPoint[] anchorPoints;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		anchorPoints = GetComponentsInChildren<AnchorPoint>();

		WhitTools.Assert(anchorPoints != null && anchorPoints.Length > 0, "object has no anchor points!");
	}

	public void HandleRelease() {
		isConnected = false;
	}

	public void HandleConnected() {
		isConnected = true;
	}

	public Vector2 GetRandomLocalAnchorPoint() {
		AnchorPoint anchorPoint = anchorPoints[UnityEngine.Random.Range(0, anchorPoints.Length)];
		return anchorPoint.transform.localPosition;
	}
}
