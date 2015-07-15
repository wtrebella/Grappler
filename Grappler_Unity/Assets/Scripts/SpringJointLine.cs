using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(LineRenderer))]
public class SpringJointLine : MonoBehaviour {
	private SpringJoint2D springJoint;
	private LineRenderer lineRenderer;

	private void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
		springJoint = GetComponent<SpringJoint2D>();
	}

	private void Update() {
		if (springJoint.enabled) DrawLineBetweenAnchors();
		else DrawNothing();
	}

	private void DrawLineBetweenAnchors() {
		Vector3 localAnchor = springJoint.anchor;
		Vector3 localConnectedAnchor = springJoint.connectedAnchor;

		Vector3 worldAnchor = springJoint.transform.TransformPoint(localAnchor);
		Vector3 worldConnectedAnchor = springJoint.connectedBody.transform.TransformPoint(localConnectedAnchor);

		lineRenderer.SetPosition(0, worldAnchor);
		lineRenderer.SetPosition(1, worldConnectedAnchor);

		lineRenderer.enabled = true;
	}

	private void DrawNothing() {
		lineRenderer.enabled = false;
	}
}
