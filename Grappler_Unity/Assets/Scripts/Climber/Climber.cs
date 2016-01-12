using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ClimberAnimator))]
[RequireComponent(typeof(ClimberMover))]
[RequireComponent(typeof(KinematicSwitcher))]
[RequireComponent(typeof(TriggerSwitcher))]
public class Climber : StateMachine {
	public Action SignalEnteredClimbingState;
	public Action SignalEnteredGrapplingState;
	public Action SignalEnteredFallingState;

	[SerializeField] private AnchorableFinder anchorableFinder;
	[SerializeField] private Grappler grappler;
	[SerializeField] private ClimberMountainCollision bodyMountainCollision;
	[SerializeField] private ClimberMountainCollision feetMountainCollision;

	private enum ClimberStates {Climbing, Grappling, Falling};

	private ClimberAnimator climberAnimator;
	private KinematicSwitcher kinematicSwitcher;
	private TriggerSwitcher triggerSwitcher;
	private ClimberMover climberMover;

	public void SetClimbingState(float place) {
		currentState = ClimberStates.Climbing;
		climberMover.StartClimbing(place);
	}

	public void SetGrapplingState() {
		currentState = ClimberStates.Grappling;
	}

	public void SetFallingState() {
		currentState = ClimberStates.Falling;
	}

	public bool IsClimbing() {
		return (ClimberStates)currentState == ClimberStates.Climbing;
	}

	public bool IsGrappling() {
		return (ClimberStates)currentState == ClimberStates.Grappling;
	}

	public bool IsFalling() {
		return (ClimberStates)currentState == ClimberStates.Falling;
	}

	private void Awake() {
		grappler.SignalEnteredConnectedState += HandleGrapplerEnteredConnectedState;
		grappler.SignalEnteredDisconnectedState += HandleGrapplerEnteredDisconnectedState;
		bodyMountainCollision.SignalMountainCollision += HandleBodyMountainCollision;
		feetMountainCollision.SignalMountainCollision += HandleFeetMountainCollision;
		triggerSwitcher = GetComponent<TriggerSwitcher>();
		climberAnimator = GetComponent<ClimberAnimator>();
		kinematicSwitcher = GetComponent<KinematicSwitcher>();
		climberMover = GetComponent<ClimberMover>();
	}

	private void Start() {
		StartCoroutine(WaitThenClimb());
	}

	private IEnumerator WaitThenClimb() {
		yield return new WaitForSeconds(1);
		SetClimbingState(0);
	}

	private void HandleBodyMountainCollision(MountainChunk mountainChunk, Vector2 point) {
		if (!IsClimbing()) SetClimbingState(GetPlaceNearestPoint(mountainChunk, point));
	}

	private void HandleFeetMountainCollision(MountainChunk mountainChunk, Vector2 point) {
		if (!IsClimbing()) SetClimbingState(GetPlaceNearestPoint(mountainChunk, point));
	}

	private float GetPlaceNearestPoint(MountainChunk mountainChunk, Vector2 point) {
		float goalPlace;
		Anchorable anchorable;
		if (anchorableFinder.FindAnchorableInCircle(out anchorable)) {
			// 1. get the Point of the anchorable
			Point linePoint = anchorable.linePoint;
			
			// 2. find out if it's this Point and the NEXT or the PREVIOUS
			Point pointA = null;
			Point pointB = null;
			if (linePoint.y > point.y) {
				int linePointIndex = mountainChunk.GetIndexOfLinePoint(linePoint);
				if (linePointIndex == 0) return 0;
				else {
					pointA = mountainChunk.GetLinePoint(linePointIndex - 1);
					pointB = linePoint;
				}
			}
			else if (linePoint.y < point.y) {
				int linePointIndex = mountainChunk.GetIndexOfLinePoint(linePoint);
				if (linePointIndex == mountainChunk.GetListOfLinePoints().Count - 1) {
					return 1;
				}
				else {
					pointA = linePoint;
					pointB = mountainChunk.GetLinePoint(linePointIndex + 1);
				}
			}
			
			// 3. float goalPlace = pointAPlace + (point - pointA).magnitude / (pointB - pointA).magnitude
			float pointAPlace = mountainChunk.GetPlaceAtPoint(pointA);
			goalPlace = pointAPlace + (point - pointA.pointVector).magnitude / (pointB.pointVector - pointA.pointVector).magnitude;
			return goalPlace;
		}
		return 0;
	}

	private void HandleGrapplerEnteredConnectedState() {
		StopClimbingIfNeeded();
		SetGrapplingState();
	}

	private void HandleGrapplerEnteredDisconnectedState() {
		StopClimbingIfNeeded();
		SetFallingState();
	}

	private void StopClimbingIfNeeded() {
		if (IsClimbing()) climberMover.StopClimbing();
	}

	private void Climbing_EnterState() {
		if (SignalEnteredClimbingState != null) SignalEnteredClimbingState();

		kinematicSwitcher.SetKinematic();
		triggerSwitcher.SetAsTrigger(0.3f);
		grappler.ReleaseGrappleIfConnected();
		climberAnimator.PlayClimbingAnimations();
	}

	private void Grappling_EnterState() {
		if (SignalEnteredGrapplingState != null) SignalEnteredGrapplingState();

		triggerSwitcher.SetAsNonTrigger(0.3f);
		kinematicSwitcher.SetNonKinematic();
		climberMover.StopClimbing();
		climberAnimator.PlayGrapplingAnimations();
	}

	private void Falling_EnterState() {
		if (SignalEnteredFallingState != null) SignalEnteredFallingState();

		climberAnimator.PlayFallingAnimations();
	}
}
