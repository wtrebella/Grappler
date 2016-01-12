using UnityEngine;
using System.Collections;
using System;

public class ClimberController : MonoBehaviour {
	[SerializeField] private Climber climber;
	[SerializeField] private AnchorableFinder anchorableFinder;

	public void StartClimbing() {
		climber.StartClimbing();
	}

	public void StopClimbing() {
		climber.StopClimbing();
	}

	public void HandleFallingSwipe(Vector2 swipeDirection, float swipeMagnitude) {

	}
	
	public void HandleClimbingSwipe(Vector2 swipeDirection, float swipeMagnitude) {

	}
	
	public void HandleGrapplingSwipe(Vector2 swipeDirection, float swipeMagnitude) {

	}

	public void HandleTap() {

	}
}
