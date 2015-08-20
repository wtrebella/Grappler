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
	
	private void Update() {
		DrawRope();
	}
	
	private void DrawRope() {
		Vector2 startPoint = grappleRopeEndPoints.GetStartPoint();
		Vector2 endPoint = grappleRopeEndPoints.GetEndPoint();
		
		lineRenderer.SetPosition(0, startPoint);
		lineRenderer.SetPosition(1, endPoint);
		
		if (!lineRenderer.enabled) lineRenderer.enabled = true;
	}
	
	private void DrawNothing() {
		if (lineRenderer.enabled) lineRenderer.enabled = false;
	}
}
