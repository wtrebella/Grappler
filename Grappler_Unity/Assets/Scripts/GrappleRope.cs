using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpringJoint2D))]
public class GrappleRope : StateMachine {
	public Action Signal_Retracted_EnterState;
	public Action Signal_Connected_EnterState;

	public Action Signal_Retracted_UpdateState;
	public Action Signal_Connected_UpdateState;
	
	private enum GrappleRopeStates {Retracted, Connected}
	private Anchorable connectedAnchorable;
	private SpringJoint2D springJoint;

	public bool IsRetracted() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Retracted;
	}

	public bool IsConnected() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Connected;
	}
	
	public void Connect(Anchorable anchorable) {
		WhitTools.Assert(!HasConnectedAnchorable(), "already connected to something else! release before connecting.");
		
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
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
		currentState = GrappleRopeStates.Retracted;
	}

	private void Retracted_EnterState() {
		if (Signal_Retracted_EnterState != null) Signal_Retracted_EnterState();
	}

	private void Retracted_UpdateState() {
		if (Signal_Retracted_UpdateState != null) Signal_Retracted_UpdateState();
	}

	private void Connected_EnterState() {
		if (Signal_Connected_EnterState != null) Signal_Connected_EnterState();
	}

	private void Connected_UpdateState() {
		if (Signal_Connected_UpdateState != null) Signal_Connected_UpdateState();
	}

	private bool HasConnectedAnchorable() {
		return connectedAnchorable != null;
	}

	private Vector2 GetConnectedAnchorablePoint() {
		WhitTools.Assert(HasConnectedAnchorable(), "hasn't ever connected to any anchorables!");
		
		return springJoint.GetConnectedAnchorInWorldPosition();
	}
}
