using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpringJoint2D))]
public class Grappler : StateMachine {
	[SerializeField] private Anchorable square;

	private enum GrapplerStates {Standing, Falling, Grappling};
	private SpringJoint2D springJoint;

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
		currentState = GrapplerStates.Falling;
	}

	private void Falling_UpdateState() {
		if (GetGrappleButton()) ConnectGrapple();
	}
	
	private void Grappling_UpdateState() {
		if (!GetGrappleButton()) ReleaseGrapple();
	}

	private bool GetGrappleButton() {
		return Input.GetKey(KeyCode.Space);
	}

	private void ReleaseGrapple() {
		springJoint.enabled = false;
		currentState = GrapplerStates.Falling;
	}

	private void ConnectGrapple() {
		springJoint.connectedBody = square.rigidbody2D;
		springJoint.connectedAnchor = square.GetRandomLocalAnchorPoint();
		springJoint.enabled = true;
		currentState = GrapplerStates.Grappling;
	}
}
