using UnityEngine;
using System.Collections;
using System;

public class GrapplerArmEndPoints : MonoBehaviour {
	public Action SignalArmEndPointsUpdated;

	[SerializeField] Transform armSocket;
	[SerializeField] Transform topOfHead;
	[SerializeField] GrappleRope grappleRope;

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

	private void Start() {
		grappleRope.Signal_Connected_UpdateState += Connected_UpdateState;
		grappleRope.Signal_Retracted_UpdateState += Retracted_UpdateState;
		grappleRope.Signal_FreeFlowing_UpdateState += FreeFlowing_UpdateState;

		startTransform = new GameObject("Grapple Arm Start Transform").transform;
		endTransform = new GameObject("Grapple Arm End Transform").transform;
		startTransform.parent = transform;
		endTransform.parent = transform;
	}

	private void Retracted_UpdateState() {
		startTransform.position = endTransform.position = armSocket.transform.position;
		if (SignalArmEndPointsUpdated != null) SignalArmEndPointsUpdated();
	}

	private void Connected_UpdateState() {
		startTransform.position = armSocket.transform.position;
		endTransform.position = GetGrabPoint();
		if (SignalArmEndPointsUpdated != null) SignalArmEndPointsUpdated();
	}

	private void FreeFlowing_UpdateState() {
		startTransform.position = endTransform.position = armSocket.transform.position;
		if (SignalArmEndPointsUpdated != null) SignalArmEndPointsUpdated();
	}

	private Vector2 GetGrabPoint() {
		Vector2 top = topOfHead.position;
		Vector2 ropeVector = grappleRope.GetVector();
		Vector2 vectorToGrabPoint = ropeVector.normalized * Mathf.Min(0.3f, ropeVector.magnitude);
		Vector2 grabPoint = top += vectorToGrabPoint;
		return grabPoint;
	}
}
