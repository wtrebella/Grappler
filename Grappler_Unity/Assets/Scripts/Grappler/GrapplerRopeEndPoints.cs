using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(GrappleRope))]
public class GrapplerRopeEndPoints : MonoBehaviour {
	public Action SignalRopeEndPointsUpdated;

	[SerializeField] private float grabPointMaxDistance = 0.3f;
	[SerializeField] private Transform topOfHead;

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

	public Vector2 GetGrabPoint() {
		Vector2 top = topOfHead.position;
		Vector2 ropeVector = grappleRope.GetVector();
		Vector2 vectorToGrabPoint = ropeVector.normalized * Mathf.Min(grabPointMaxDistance, ropeVector.magnitude);
		Vector2 grabPoint = top += vectorToGrabPoint;
		return grabPoint;
	}

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		grappleRope = GetComponent<GrappleRope>();

		grappleRope.Signal_Connected_UpdateState += Connected_UpdateState;
		grappleRope.Signal_Retracted_UpdateState += Retracted_UpdateState;
		grappleRope.Signal_FreeFlowing_UpdateState += FreeFlowing_UpdateState;

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
