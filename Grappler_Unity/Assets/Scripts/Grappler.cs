using UnityEngine;
using System.Collections;
using System.Linq;
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
		// i think what i really need to do is have a multiple raycast thing, starting at 45 degrees,
		// and if nothing is found, alternating between about 5 degrees up and down until
		// 1. something(s) is found (if multiple, choose the furthest one
		// 2. the angle reaches past 90 or -90 (in essence, these would be behind, which we don't want)

		Anchorable anchorable = GetClosestInFrontAnchorable();
		if (anchorable == null) return;

		springJoint.connectedBody = anchorable.rigidbody2D;
		springJoint.connectedAnchor = anchorable.GetRandomLocalAnchorPoint();
		springJoint.enabled = true;
		currentState = GrapplerStates.Grappling;
	}

	private List<Transform> GetAllNearbyAnchorableTransforms() {
		var colliders = Physics2D.OverlapCircleAll(transform.position, maxRopeLength, 1 << LayerMask.NameToLayer("Anchorable"));
		List<Transform> transformsList = new List<Transform>();
		foreach (Collider2D collider in colliders) transformsList.Add(collider.transform);
		return transformsList;
	}

	private Anchorable GetBestFitAnchorable() {
		var transforms = GetAllNearbyAnchorableTransforms();
		if (transforms.Count == 0) return null;

		List<Transform> anchorablesInFrontOfPlayer = transforms.Copy();
		anchorablesInFrontOfPlayer.RemoveItemsWithXValsUnder(transform.position.x);
		Transform highestInFrontAnchorable = anchorablesInFrontOfPlayer.GetItemWithHighestY();
		if (highestInFrontAnchorable != null) return highestInFrontAnchorable.GetComponent<Anchorable>();

		List<Transform> anchorablesBehindPlayer = transforms.Copy();
		anchorablesBehindPlayer.RemoveItemsWithXValsOver(transform.position.x);
		Transform highestBehindAnchorable = anchorablesBehindPlayer.GetItemWithHighestY();
		if (highestBehindAnchorable != null) return highestBehindAnchorable.GetComponent<Anchorable>();

		return null;
	}

	private Anchorable GetClosestInFrontAnchorable() {
		var transforms = GetAllNearbyAnchorableTransforms();
		if (transforms.Count == 0) return null;

		transforms.RemoveItemsWithXValsUnder(transform.position.x);

		Transform anchorableTransform = transforms.GetItemClosestTo(transform.position);
		if (anchorableTransform != null) return anchorableTransform.GetComponent<Anchorable>();

		return null;
	}
}
