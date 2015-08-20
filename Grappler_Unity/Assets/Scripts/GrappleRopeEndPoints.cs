using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(GrappleRope))]
public class GrappleRopeEndPoints : MonoBehaviour {
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

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		grappleRope = GetComponent<GrappleRope>();

		grappleRope.Signal_Retracting_EnterState += Retracting_EnterState;

		grappleRope.Signal_Connected_UpdateState += Connected_UpdateState;
		grappleRope.Signal_Retracted_UpdateState += Retracted_UpdateState;
		grappleRope.Signal_Retracting_UpdateState += Retracting_UpdateState;

		startTransform = new GameObject("Grapple Rope Start Transform").transform;
		endTransform = new GameObject("Grapple Rope End Transform").transform;
		startTransform.parent = transform;
		endTransform.parent = transform;
	}
	
	private void Retracting_EnterState() {
		localEndPointAtRelease = endTransform.localPosition;
	}

	private void Retracting_UpdateState() {
		float retractionPercent = grappleRope.GetRetractionPercent();
		float protractionPercent = 1 - retractionPercent;
		Vector2 adjustedLocalEndPoint = localEndPointAtRelease * protractionPercent;
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		startTransform.position = startAnchor;
		endTransform.localPosition = adjustedLocalEndPoint;
	}

	private void Retracted_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		startTransform.position = endTransform.position = startAnchor;
	}

	private void Connected_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		Vector2 endAnchor = springJoint.GetConnectedAnchorInWorldPosition();
		startTransform.position = startAnchor;
		endTransform.position = endAnchor;
	}
}
