using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(SpringJoint2D))]
public class Grappler : StateMachine {
	[SerializeField] private CircleRaycaster circleRaycaster;
	private enum GrapplerStates {Standing, Falling, Grappling};
	private SpringJoint2D springJoint;

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
		currentState = GrapplerStates.Falling;
	}

	private void Falling_UpdateState() {
		if (GetGrappleButton()) {
			Anchorable anchorable;
			if (FindAnchorable(out anchorable)) ConnectGrapple(anchorable);
		}
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

	private void ConnectGrapple(Anchorable anchorable) {
		springJoint.connectedBody = anchorable.rigidbody2D;
		springJoint.connectedAnchor = anchorable.GetRandomLocalAnchorPoint();
		springJoint.enabled = true;
		currentState = GrapplerStates.Grappling;
	}

	private bool FindAnchorable(out Anchorable anchorable) {
		Collider2D foundCollider;
		if (circleRaycaster.FindCollider(out foundCollider)) {
			anchorable = foundCollider.GetComponent<Anchorable>();
			return true;
		}
		anchorable = null;
		return false;
	}
}
