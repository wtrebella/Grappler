using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpringJoint2D))]
[RequireComponent(typeof(GrappleSpringJointRopeEndPoints))]
public class GrappleRope : StateMachine {
	public SpringJointAttributes retractedAttributes;
	public SpringJointAttributes connectedAttributes;
	public SpringJointAttributes freeFlowingAttributes;

	public Action Signal_Retracted_EnterState;
	public Action Signal_Connected_EnterState;
	public Action Signal_FreeFlowing_EnterState;

	public Action Signal_Retracted_UpdateState;
	public Action Signal_Connected_UpdateState;
	public Action Signal_FreeFlowing_UpdateState;
	
	private enum GrappleRopeStates {Retracted, FreeFlowing, Connected}
	private Anchorable connectedAnchorable;
	private GrappleSpringJointRopeEndPoints ropeEndPoints;
	private SpringJoint2D springJoint;
	private Rigidbody2D misfireBody;

	public bool IsRetracted() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Retracted;
	}

	public bool IsConnected() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Connected;
	}

	public bool IsFreeFlowing() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.FreeFlowing;
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
	
	public void Connect(Anchorable anchorable) {
		WhitTools.Assert(!HasConnectedAnchorable(), "already connected to something else! release before connecting.");

		springJoint.ApplyAttributes(connectedAttributes);
		springJoint.connectedBody = anchorable.rigid;
		springJoint.connectedAnchor = anchorable.GetAnchorPoint();
		springJoint.enabled = true;
		connectedAnchorable = anchorable;
		anchorable.HandleConnected();
		currentState = GrappleRopeStates.Connected;
	}

	public void Misfire(Vector2 direction) {
		WhitTools.Assert(!HasConnectedAnchorable(), "can't misfire if already connected!");

		springJoint.ApplyAttributes(freeFlowingAttributes);
		misfireBody.isKinematic = true;
		misfireBody.transform.position = springJoint.GetAnchorInWorldPosition() + direction * 3;
		misfireBody.isKinematic = false;
		springJoint.enabled = true;
		springJoint.connectedBody = misfireBody;
		springJoint.connectedAnchor = Vector2.zero;
		currentState = GrappleRopeStates.FreeFlowing;
	}

	public void Release() {
		WhitTools.Assert(HasConnectedAnchorable(), "not connected! can't release if not connected.");

		springJoint.ApplyAttributes(retractedAttributes);
		springJoint.enabled = false;
		connectedAnchorable.HandleRelease();
		connectedAnchorable = null;
		currentState = GrappleRopeStates.Retracted;
	}
	
	private void Awake() {
		misfireBody = new GameObject("Misfire Body").AddComponent<Rigidbody2D>();
		misfireBody.mass = 0.1f;
		misfireBody.isKinematic = true;
		ropeEndPoints = GetComponent<GrappleSpringJointRopeEndPoints>();
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
		currentState = GrappleRopeStates.Retracted;
	}

	private void Retracted_UpdateState() {
		if (Signal_Retracted_UpdateState != null) Signal_Retracted_UpdateState();
	}

	private void FreeFlowing_UpdateState() {
		if (Signal_FreeFlowing_UpdateState != null) Signal_FreeFlowing_UpdateState();
		if (ropeEndPoints.EndPointsAreVeryClose()) currentState = GrappleRopeStates.Retracted;
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

	private void FreeFlowing_EnterState() {
		if (Signal_FreeFlowing_EnterState != null) Signal_FreeFlowing_EnterState();
	}

	private bool HasConnectedAnchorable() {
		return connectedAnchorable != null;
	}

	private Vector2 GetConnectedAnchorablePoint() {
		WhitTools.Assert(HasConnectedAnchorable(), "hasn't ever connected to any anchorables!");
		
		return springJoint.GetConnectedAnchorInWorldPosition();
	}
}
