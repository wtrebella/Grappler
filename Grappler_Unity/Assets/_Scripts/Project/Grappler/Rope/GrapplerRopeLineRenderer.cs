using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

[RequireComponent(typeof(GrapplerRopeEndPoints))]
public class GrapplerRopeLineRenderer : MonoBehaviour {
	[SerializeField] private float width = 2;
	[SerializeField] private Material material;

	private VectorLine line;
	private GrapplerRopeEndPoints grappleRopeEndPoints;
	
	private void Start() {
		grappleRopeEndPoints = GetComponent<GrapplerRopeEndPoints>();
		grappleRopeEndPoints.SignalRopeEndPointsUpdated += HandleRopeEndPointsUpdated;
		InitLine();
	}
	
	private void HandleRopeEndPointsUpdated() {
		DrawRope();
	}
	
	private void DrawRope() {
		Vector3 startPoint = (Vector3)grappleRopeEndPoints.GetStartPoint();
		Vector3 endPoint = (Vector3)grappleRopeEndPoints.GetEndPoint();
		startPoint.z = -0.01f;
		endPoint.z = -0.01f;
		line.points3.Clear();
		line.points3.Add(startPoint);
		line.points3.Add(endPoint);
		line.Draw3D();
	}
	
	private void InitLine() {
		VectorManager.useDraw3D = true;
		line = new VectorLine("Rope Line", new List<Vector3>(), width, LineType.Continuous, Joins.Weld);
		line.material = material;
		line.layer = LayerMask.NameToLayer("Default");
		line.color = new Color32(54, 54, 54, 255);
	}

	private void OnDestroy() {
		if (line != null && line.rectTransform != null) Destroy(line.rectTransform.gameObject);
	}
}
