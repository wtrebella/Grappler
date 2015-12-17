using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

[RequireComponent(typeof(GrappleRopeEndPoints))]
public class GrappleRopeLineRenderer : MonoBehaviour {
	[SerializeField] private float width = 2;
	
	private VectorLine line;
	private GrappleRopeEndPoints grappleRopeEndPoints;

	private void Awake() {
		grappleRopeEndPoints = GetComponent<GrappleRopeEndPoints>();
		grappleRopeEndPoints.SignalRopeEndPointsUpdated += HandleRopeEndPointsUpdated;
		InitLine();
	}

	private void HandleRopeEndPointsUpdated() {
		DrawRope();
	}

	private void DrawRope() {
		Vector3 startPoint = grappleRopeEndPoints.GetStartPoint().ToVector3();
		Vector3 endPoint = grappleRopeEndPoints.GetEndPoint().ToVector3();
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
		line.color = new Color32(186, 150, 112, 255);
	}
}
