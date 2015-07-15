using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpringJoint2D))]
public class Grappler : StateMachine {
	[SerializeField] private float maxRopeLength = 10;
	private enum GrapplerStates {Standing, Falling, Grappling};
	private SpringJoint2D springJoint;

	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
		currentState = GrapplerStates.Falling;
		GetNearestAnchorable();
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
		Anchorable anchorable = GetNearestAnchorable();
		if (anchorable == null) return;

		springJoint.connectedBody = anchorable.rigidbody2D;
		springJoint.connectedAnchor = anchorable.GetRandomLocalAnchorPoint();
		springJoint.enabled = true;
		currentState = GrapplerStates.Grappling;
	}

	private Anchorable GetNearestAnchorable() {
		var colliders = Physics2D.OverlapCircleAll(transform.position, maxRopeLength, 1 << LayerMask.NameToLayer("Anchorable"));
		if (colliders.Length == 0) return null;

		Collider2D mostForwardCollider = null;
		float tempX = Mathf.NegativeInfinity;
		foreach (Collider2D collider in colliders) {
			if (collider.transform.position.x > tempX) {
				mostForwardCollider = collider;
				tempX = collider.transform.position.x;
			}
		}

		Anchorable anchorable = mostForwardCollider.GetComponent<Anchorable>();
		return anchorable;
	}
}
