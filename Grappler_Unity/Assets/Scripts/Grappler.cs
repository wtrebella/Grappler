using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrappleRope))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	private GrappleRope grappleRope;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrappleRope>();
		currentState = GrapplerStates.Falling;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		currentState = GrapplerStates.Dead;
	}

	private void Falling_UpdateState() {
		if (GetGrappleButton()) {
		 	if (!grappleRope.IsRetracting()) {
				ConnectGrappleIfAble();
			}
		}
	}
	
	private void Grappling_UpdateState() {
		if (!GetGrappleButton()) {
			ReleaseGrapple();
			currentState = GrapplerStates.Falling;
		}
	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
	}

	private bool GetGrappleButton() {
		return Input.GetKey(KeyCode.Space);
	}

	private void ConnectGrappleIfAble() {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable)) ConnectGrapple(anchorable);
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleRope.Connect(anchorable);
		currentState = GrapplerStates.Grappling;
	}

	private void ReleaseGrapple() {
		grappleRope.Release();
	}

	private bool FindAnchorable(out Anchorable anchorable) {
		return anchorableFinder.FindAnchorable(out anchorable);
	}
}
