﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(GrappleRope))]
public class GrappleRopeEndPoints : MonoBehaviour {
	public Action SignalRopeEndPointsUpdated;

	private GrappleRope grappleRope;
	private SpringJoint2D springJoint;
	private Transform startTransform;
	private Transform endTransform;
	private Vector2 localEndPointAtRelease;

	public Vector2 GetStartPoint() {
		return startTransform.position;
	}

	public Vector2 GetEndPoint() {
		return endTransform.position;
	}
	
	public bool EndPointsAreVeryClose() {
		return (startTransform.position - endTransform.position).magnitude < 0.1f;
	}

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		grappleRope = GetComponent<GrappleRope>();

		grappleRope.Signal_Connected_UpdateState += Connected_UpdateState;
		grappleRope.Signal_Retracted_UpdateState += Retracted_UpdateState;
		grappleRope.Signal_FreeFlowing_UpdateState += FreeFlowing_UpdateState;

		startTransform = new GameObject("Grapple Rope Start Transform").transform;
		endTransform = new GameObject("Grapple Rope End Transform").transform;
		startTransform.parent = transform;
		endTransform.parent = transform;
	}

	private void Retracted_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		startTransform.position = endTransform.position = startAnchor;
		if (SignalRopeEndPointsUpdated != null) SignalRopeEndPointsUpdated();
	}

	private void Connected_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
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
