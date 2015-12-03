using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(GrappleRopeEndPoints))]
public class GrappleRopeLineRenderer : MonoBehaviour {
	private GrappleRopeEndPoints grappleRopeEndPoints;
	private LineRenderer lineRenderer;
	
	private void Awake() {
		grappleRopeEndPoints = GetComponent<GrappleRopeEndPoints>();
		lineRenderer = GetComponent<LineRenderer>();
	}
	
	private void FixedUpdate() {
		DrawRope();
	}
	
	private void DrawRope() {
		Vector3 startPoint = grappleRopeEndPoints.GetStartPoint().ToVector3();
		Vector3 endPoint = grappleRopeEndPoints.GetEndPoint().ToVector3();
		startPoint.z = -0.5f;
		endPoint.z = -0.5f;
		lineRenderer.SetWidth(0.075f, 0.075f);
		lineRenderer.sortingOrder = 5000;
		lineRenderer.SetPosition(0, startPoint);
		lineRenderer.SetPosition(1, endPoint);
		
		if (!lineRenderer.enabled) lineRenderer.enabled = true;
	}
	
	private void DrawNothing() {
		if (lineRenderer.enabled) lineRenderer.enabled = false;
	}
}
