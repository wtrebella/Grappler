using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrappleConnector))]
public class Grappler : StateMachine {
	private GrappleConnector grappleConnector;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling};

	private void Awake() {
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleConnector = GetComponent<GrappleConnector>();
		currentState = GrapplerStates.Falling;
	}

	private void Falling_UpdateState() {
		if (GetGrappleButton()) ConnectGrappleIfAnchorableAvailable();
	}
	
	private void Grappling_UpdateState() {
		if (!GetGrappleButton()) ReleaseGrapple();
	}

	private bool GetGrappleButton() {
		return Input.GetKey(KeyCode.Space);
	}

	private void ConnectGrappleIfAnchorableAvailable() {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable)) ConnectGrapple(anchorable);
	}

	private void ConnectGrapple(Anchorable anchorable) {
		grappleConnector.Connect(anchorable);
		currentState = GrapplerStates.Grappling;
	}

	private void ReleaseGrapple() {
		grappleConnector.Release();
		currentState = GrapplerStates.Falling;
	}

	private bool FindAnchorable(out Anchorable anchorable) {
		return anchorableFinder.FindAnchorable(out anchorable);
	}
}
