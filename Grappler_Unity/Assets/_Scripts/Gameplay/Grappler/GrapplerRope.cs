using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpringJointAttributeCooldownLerper))]
[RequireComponent(typeof(GrapplerRopeEndPoints))]
public class GrapplerRope : StateMachine {
	public Action Signal_Retracted_EnterState;
	public Action Signal_Connected_EnterState;

	public Action Signal_Retracted_UpdateState;
	public Action Signal_Connected_UpdateState;

	[SerializeField] private SpringJoint2D springJoint;

	private enum GrappleRopeStates {Retracted, Connected}
	private SpringJointAttributeCooldownLerper springJointAttributeCooldownLerper;
	private Anchorable connectedAnchorable;
	private GrapplerRopeEndPoints ropeEndPoints;

	public bool IsRetracted() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Retracted;
	}

	public bool IsConnected() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Connected;
	}

	public Vector2 GetVector() {
		return ropeEndPoints.GetEndPoint() - ropeEndPoints.GetStartPoint();
	}

	public Vector2 GetStartPoint() {
		return ropeEndPoints.GetStartPoint();
	}

	public Vector2 GetEndPoint() {
		return ropeEndPoints.GetEndPoint();
	}

	public Vector2 GetGrabPoint() {
		return ropeEndPoints.GetGrabPoint();
	}
	
	public void Connect(Anchorable anchorable) {
		WhitTools.Assert(!HasConnectedAnchorable(), "already connected to something else! release before connecting.");

		SetSpringJointAttributes();
		springJoint.connectedBody = anchorable.rigid;
		springJoint.connectedAnchor = anchorable.GetAnchorPoint();
		springJoint.enabled = true;
		connectedAnchorable = anchorable;
		anchorable.HandleConnected();
		currentState = GrappleRopeStates.Connected;
	}

	public void Release() {
		WhitTools.Assert(HasConnectedAnchorable(), "not connected! can't release if not connected.");

		springJoint.enabled = false;
		connectedAnchorable.HandleRelease();
		connectedAnchorable = null;
		currentState = GrappleRopeStates.Retracted;
	}
	
	private void Awake() {
		ropeEndPoints = GetComponent<GrapplerRopeEndPoints>();
		springJointAttributeCooldownLerper = GetComponent<SpringJointAttributeCooldownLerper>();
		springJoint.enabled = false;
		currentState = GrappleRopeStates.Retracted;
	}

	private void SetSpringJointAttributes() {
		springJoint.distance = springJointAttributeCooldownLerper.GetDistance();
		springJoint.frequency = springJointAttributeCooldownLerper.GetFrequency();
		springJoint.dampingRatio = springJointAttributeCooldownLerper.GetDampingRatio();
	}

	private void Retracted_UpdateState() {
		if (Signal_Retracted_UpdateState != null) Signal_Retracted_UpdateState();
	}

	private void Connected_UpdateState() {
		if (Signal_Connected_UpdateState != null) Signal_Connected_UpdateState();
	}

	private void Retracted_EnterState() {
		if (Signal_Retracted_EnterState != null) Signal_Retracted_EnterState();
	}

	private void Connected_EnterState() {
		if (Signal_Connected_EnterState != null) Signal_Connected_EnterState();
	}

	private bool HasConnectedAnchorable() {
		return connectedAnchorable != null;
	}

	private Vector2 GetConnectedAnchorablePoint() {
		WhitTools.Assert(HasConnectedAnchorable(), "hasn't ever connected to any anchorables!");
		
		return springJoint.GetConnectedAnchorInWorldPosition();
	}
}
