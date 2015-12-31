using UnityEngine;
using System.Collections;
using System;

public class GrapplerArmEndPoints : MonoBehaviour {
	public Action SignalArmEndPointsUpdated;

	[SerializeField] private float extraReach = 0.0f;
	[SerializeField] private float elbowLength = 0.15f;
	[SerializeField] private GrapplerArm arm;
	[SerializeField] private Transform armSocket;
	[SerializeField] private GrappleRope grappleRope;

	private Transform startTransform;
	private Transform elbowTransform;
	private Transform endTransform;

	public Vector2 GetStartPoint() {
		return startTransform.position;
	}

	public Vector2 GetEndPoint() {
		return endTransform.position;
	}

	public Vector2 GetElbowPoint() {
		return elbowTransform.position;
	}
	
	public bool EndPointsAreVeryClose() {
		return (startTransform.position - endTransform.position).magnitude < 0.1f;
	}

	private void Start() {
		grappleRope.Signal_Connected_UpdateState += Connected_UpdateState;
		grappleRope.Signal_Retracted_UpdateState += Retracted_UpdateState;
		grappleRope.Signal_FreeFlowing_UpdateState += FreeFlowing_UpdateState;

		startTransform = new GameObject("Grapple Arm Start Transform").transform;
		elbowTransform = new GameObject("Grapple Arm Elbow Transform").transform;
		endTransform = new GameObject("Grapple Arm End Transform").transform;

		startTransform.parent = transform;
		elbowTransform.parent = transform;
		endTransform.parent = transform;
	}

	private void Retracted_UpdateState() {
		startTransform.position = endTransform.position = elbowTransform.position = armSocket.transform.position;
		if (SignalArmEndPointsUpdated != null) SignalArmEndPointsUpdated();
	}

	private void Connected_UpdateState() {
		Vector2 extraReachVector = grappleRope.GetVector().normalized * extraReach;
		startTransform.position = armSocket.transform.position;
		Vector3 elbowVector = Vector2.zero;
		if (arm.GetArmType() == ArmType.Left) elbowVector = new Vector3(-1, 2, 0).normalized * elbowLength;
		else if (arm.GetArmType() == ArmType.Right) elbowVector = new Vector3(1, 2, 0).normalized * elbowLength;

		elbowVector = transform.TransformDirection(elbowVector);
		elbowTransform.position = armSocket.position + elbowVector;
		endTransform.position = grappleRope.GetGrabPoint() + extraReachVector;
		if (SignalArmEndPointsUpdated != null) SignalArmEndPointsUpdated();
	}

	private void FreeFlowing_UpdateState() {
		startTransform.position = endTransform.position = elbowTransform.position = armSocket.transform.position;
		if (SignalArmEndPointsUpdated != null) SignalArmEndPointsUpdated();
	}
}
