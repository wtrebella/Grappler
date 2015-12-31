using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

[RequireComponent(typeof(GrapplerArmEndPoints))]
public class GrapplerArmLineRenderer : MonoBehaviour {
	[SerializeField] private float width = 2;
	
	private VectorLine line;
	private GrapplerArmEndPoints grappleArmEndPoints;
	
	private void Awake() {
		grappleArmEndPoints = GetComponent<GrapplerArmEndPoints>();
		grappleArmEndPoints.SignalArmEndPointsUpdated += HandleArmEndPointsUpdated;
		InitLine();
	}
	
	private void HandleArmEndPointsUpdated() {
		DrawArm();
	}
	
	private void DrawArm() {
		Vector3 startPoint = grappleArmEndPoints.GetStartPoint().ToVector3();
		Vector3 elbowPoint = grappleArmEndPoints.GetElbowPoint().ToVector3();
		Vector3 endPoint = grappleArmEndPoints.GetEndPoint().ToVector3();
		startPoint.z = -0.02f;
		elbowPoint.z = -0.02f;
		endPoint.z = -0.02f;
		line.points3.Clear();
		line.points3.Add(startPoint);
		line.points3.Add(elbowPoint);
		line.points3.Add(endPoint);
		line.Draw3D();
	}
	
	private void InitLine() {
		VectorManager.useDraw3D = true;
		line = new VectorLine("Arm Line", new List<Vector3>(), width, LineType.Continuous, Joins.Weld);
		line.layer = LayerMask.NameToLayer("Default");
		line.color = new Color32(90, 90, 90, 255);
	}
}
