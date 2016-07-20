using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Anchorable : MonoBehaviour {
	private static int currentAnchorableID = 0;

	public int anchorableID {get; private set;}

	public bool isConnected {get; private set;}
	public Rigidbody2D rigid {get; private set;}
	private AnchorPoint anchorPoint;

	private void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		anchorPoint = GetComponentInChildren<AnchorPoint>();
		anchorableID = currentAnchorableID++;
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
