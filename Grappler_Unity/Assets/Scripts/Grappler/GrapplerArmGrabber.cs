using UnityEngine;
using System.Collections;

public class GrapplerArmGrabber : MonoBehaviour {
	[SerializeField] Transform grabPoint;
	[SerializeField] HingeJoint2D grabHingeJoint;

	public void Grab() {
		grabHingeJoint.enabled = true;
	}

	public void Release() {
		grabHingeJoint.enabled = false;
	}

	private void Awake() {
		InitializeHingeJoint();
	}

	private void InitializeHingeJoint() {
		Vector2 connectedAnchor = grabHingeJoint.connectedAnchor;
		connectedAnchor.y = grabPoint.localPosition.y;
		grabHingeJoint.connectedAnchor = connectedAnchor;
	}
}
