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
	}

	private void Connected_UpdateState() {
		Vector2 startAnchor = springJoint.GetAnchorInWorldPosition();
		Vector2 endAnchor = springJoint.GetConnectedAnchorInWorldPosition();
		startTransform.position = startAnchor;
		endTransform.position = endAnchor;
	}

	private void FreeFlowing_UpdateState() {

	}
}
