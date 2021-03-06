﻿using UnityEngine;
using System.Collections;
using System;

public class GrapplerRopeEndPoints : MonoBehaviour {
	public Action SignalRopeEndPointsUpdated;

	[SerializeField] private Transform grabPoint;
	[SerializeField] private Transform topOfHead;
	[SerializeField] private SpringJoint2D springJoint;

	private GrapplerRope grappleRope;
	private Transform startTransform;
	private Transform endTransform;
	private Vector2 localEndPointAtRelease;

	public Vector2 GetStartPoint() {
		return startTransform.position;
	}

	public Vector2 GetEndPoint() {
		return endTransform.position;
	}

	public float GetDistanceBetweenEndPoints() {
		return (endTransform.position - startTransform.position).magnitude;
	}
	
	public bool EndPointsAreVeryClose() {
		return (startTransform.position - endTransform.position).magnitude < 0.1f;
	}

	public Vector2 GetGrabPoint() {
		return grabPoint.position;
	}

	private void Awake() {
		grappleRope = GetComponent<GrapplerRope>();

		grappleRope.Signal_Connected_UpdateState += Connected_UpdateState;
		grappleRope.Signal_Retracted_UpdateState += Retracted_UpdateState;

		startTransform = new GameObject("Grappler Rope Start Transform").transform;
		endTransform = new GameObject("Grappler Rope End Transform").transform;
		startTransform.parent = transform;
		endTransform.parent = transform;
	}

	private void Retracted_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		startTransform.position = endTransform.position = startAnchor;
		if (SignalRopeEndPointsUpdated != null) SignalRopeEndPointsUpdated();
	}

	private void Connected_UpdateState() {
		Vector2 startAnchor = GetGrabPoint();
		Vector2 endAnchor = springJoint.GetConnectedAnchorInWorldPosition();
		startTransform.position = startAnchor;
		endTransform.position = endAnchor;
		if (SignalRopeEndPointsUpdated != null) SignalRopeEndPointsUpdated();
	}

	private void FreeFlowing_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		Vector2 endAnchor = springJoint.GetConnectedAnchorInWorldPosition();
		startTransform.position = startAnchor;
		endTransform.position = endAnchor;
		if (SignalRopeEndPointsUpdated != null) SignalRopeEndPointsUpdated();
	}
}
