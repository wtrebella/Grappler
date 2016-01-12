using UnityEngine;
using System.Collections;
using System;

public class GrapplerController : MonoBehaviour {
	public Action SignalConnected;
	public Action SignalDisconnected;

	[SerializeField] private Grappler grappler;
	[SerializeField] private AnchorableFinder anchorableFinder;

	public void HandleFallingSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		Anchorable anchorable;
		
		if (anchorableFinder.FindAnchorableInDirection(out anchorable, swipeDirection)) ConnectGrapplerIfReady(anchorable);
		else grappler.Misfire(swipeDirection);
	}

	public void HandleClimbingSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		Anchorable anchorable;
		
		if (anchorableFinder.FindAnchorableInCircle(out anchorable)) ConnectGrapplerIfReady(anchorable);
	}

	public void HandleGrapplingSwipe(Vector2 swipeDirection, float swipeMagnitude) {
		DisconnectGrapplerIfReady();
	}

	public void HandleTap() {
		DisconnectGrapplerIfReady();
	}
	
	private void DisconnectGrapplerIfReady() {
		if (grappler.ReadyToDisconnect()) {
			grappler.ReleaseGrapple();
			if (SignalDisconnected != null) SignalDisconnected();
		}
	}
	
	private void ConnectGrapplerIfReady(Anchorable anchorable) {
		if (grappler.ReadyToConnect()) {
			grappler.Connect(anchorable);
			if (SignalConnected != null) SignalConnected();
		}
	}
}
