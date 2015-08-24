using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AnchorableFinder))]
[RequireComponent(typeof(GrappleRope))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grappler : StateMachine {
	[SerializeField] private BuildingGenerator buildingGenerator;
	[SerializeField] private Vector2 startOffset = new Vector2(-40, 50);

	private GrappleRope grappleRope;
	private AnchorableFinder anchorableFinder;
	private enum GrapplerStates {Falling, Grappling, Dead};

	private void Awake() {
		buildingGenerator.SignalCreatedFirstBuilding += HandleCreatedFirstBuilding;
		anchorableFinder = GetComponent<AnchorableFinder>();
		grappleRope = GetComponent<GrappleRope>();
		currentState = GrapplerStates.Falling;
	}

	private void HandleCreatedFirstBuilding(Building building) {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		Vector2 newPosition = building.topLeftCorner + startOffset;
		r.isKinematic = true;
		transform.position = newPosition;
		r.isKinematic = false;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (CurrentStateIs(GrapplerStates.Dead)) return;

		EnableRotation();
		Tumble();
		currentState = GrapplerStates.Dead;
	}

	private void Falling_UpdateState() {
		if (GetGrappleKey()) ConnectGrappleIfAble();
	}
	
	private void Grappling_UpdateState() {
		if (!GetGrappleKey()) {
			ReleaseGrapple();
			currentState = GrapplerStates.Falling;
		}
	}

	private void Dead_EnterState() {
		if (grappleRope.IsConnected()) ReleaseGrapple();
	}

	private bool GetGrappleKey() {
		return Input.GetKey(KeyCode.Space) || WhitInput.AtLeastOneTouchDown();
	}

	private void ConnectGrappleIfAble() {
		Anchorable anchorable;
		if (FindAnchorable(out anchorable)) {
			if (grappleRope.IsRetracted()) {
				ConnectGrapple(anchorable);
				currentState = GrapplerStates.Grappling;
			}
		}
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

	private void EnableRotation() {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		r.constraints = RigidbodyConstraints2D.None;
	}

	private void Tumble() {
		Rigidbody2D r = GetComponent<Rigidbody2D>();
		Vector2 force = new Vector2(10, 0);
		Vector2 position = new Vector2(0, 1);
		r.AddForceAtPosition(force, position, ForceMode2D.Impulse);
	}

	private bool CurrentStateIs(GrapplerStates grapplerState) {
		return (GrapplerStates)currentState == grapplerState;
	}
}
