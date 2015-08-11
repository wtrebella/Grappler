using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrappleConnector))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	private GrappleConnector grappleConnector;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleConnector = GetComponent<GrappleConnector>();
		currentState = GrapplerStates.Falling;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		currentState = GrapplerStates.Dead;
	}

	private void Falling_UpdateState() {
		if (GetGrappleButton()) {
			ConnectGrappleIfAble();
			currentState = GrapplerStates.Grappling;
		}
	}
	
	private void Grappling_UpdateState() {
		if (!GetGrappleButton()) {
			ReleaseGrapple();
			currentState = GrapplerStates.Falling;
		}
	}

	private void Dead_EnterState() {
		ReleaseGrapple();
	}

	private bool GetGrappleButton() {
		return Input.GetKey(KeyCode.Space);
	}

	private void ConnectGrappleIfAble() {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable)) ConnectGrapple(anchorable);
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleConnector.Connect(anchorable);
	}

	private void ReleaseGrapple() {
		grappleConnector.Release();
	}

	private bool FindAnchorable(out Anchorable anchorable) {
		return anchorableFinder.FindAnchorable(out anchorable);
	}
}
