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

	public void HandleRightSwipe(Player player) {
		
	}
	
	public void HandleLeftSwipe(Player player) {
		
	}
	
	public void HandleUpSwipe(Player player) {
		
	}
	
	public void HandleDownSwipe(Player player) {
		
	}

	public void HandleTap(Player player) {

	}
}
