using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SpringJoint2D))]
public class GrappleRope : StateMachine {
	public Action Signal_Retracted_EnterState;
	public Action Signal_Retracting_EnterState;
	public Action Signal_Connected_EnterState;

	public Action Signal_Retracted_UpdateState;
	public Action Signal_Retracting_UpdateState;
	public Action Signal_Connected_UpdateState;

	[SerializeField] private float retractionDuration = 0.5f;

	private enum GrappleRopeStates {Retracted, Retracting, Connected}
	private Anchorable connectedAnchorable;
	private SpringJoint2D springJoint;
	private float retractionTimer = 0;

	public bool IsRetracting() {
		return (GrappleRopeStates)currentState == GrappleRopeStates.Retracting;
	}

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
		currentState = GrappleRopeStates.Retracting;
	}
	
	public float GetRetractionPercent() {
		float clampedRetractionTimer = Mathf.Clamp(retractionTimer, 0, retractionDuration);
		float percent = clampedRetractionTimer / retractionDuration;
		return percent;
	}
	
	private void Awake() {
		springJoint = GetComponent<SpringJoint2D>();
		springJoint.enabled = false;
		currentState = GrappleRopeStates.Retracted;
	}

	private void Retracting_EnterState() {
		ResetRetractionTimer();
		if (Signal_Retracting_EnterState != null) Signal_Retracting_EnterState();
	}
	
	private void Retracting_UpdateState() {
		UpdateRetraction();
		if (Signal_Retracting_UpdateState != null) Signal_Retracting_UpdateState();
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

	private void ResetRetractionTimer() {
		retractionTimer = 0;
	}

	private void UpdateRetraction() {
		retractionTimer += Time.deltaTime;
		if (retractionTimer >= retractionDuration) currentState = GrappleRopeStates.Retracted;
	}
}
