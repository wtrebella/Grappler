using UnityEngine;
using System.Collections;
using Vectrosity;
using System.Collections.Generic;

[RequireComponent(typeof(GrapplerArmEndPoints))]
public class GrapplerArmLineRenderer : MonoBehaviour {
	[SerializeField] private Transform upperArm;
	[SerializeField] private Transform lowerArm;
	
	[SerializeField] private float width = 2;
	
	private VectorLine line;
	private GrapplerArmEndPoints grappleArmEndPoints;
	
	private void Awake() {
		grappleArmEndPoints = GetComponent<GrapplerArmEndPoints>();
		grappleArmEndPoints.SignalArmEndPointsUpdated += HandleArmEndPointsUpdated;
	}
	
	private void HandleArmEndPointsUpdated() {
		DrawArm();
	}
	
	private void DrawArm() {
		Vector3 lowerArmVector = grappleArmEndPoints.GetLowerArmVector().ToVector3();
		float lowerArmAngle = Vector3.Angle(lowerArmVector, transform.TransformDirection(Vector3.right)) + 90;
		Vector3 lowerArmEulers = lowerArm.transform.localEulerAngles;
		lowerArmEulers.z = lowerArmAngle;
		lowerArm.localEulerAngles = lowerArmEulers;
	}
}
