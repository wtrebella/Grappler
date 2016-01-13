using UnityEngine;
using System.Collections;
using System;

public class GrapplerController : MonoBehaviour {
	public Action SignalConnected;
	public Action SignalDisconnected;

	[SerializeField] private Grappler grappler;
	[SerializeField] private AnchorableFinder anchorableFinder;
	
	public void HandleRightSwipe(Player player) {
		Anchorable anchorable;

		if (player.IsClimbing()) {
			if (anchorableFinder.FindAnchorableInCircle(out anchorable)) ConnectGrapplerIfReady(anchorable);
		}
	}

	public void HandleLeftSwipe(Player player) {
		
	}

	public void HandleUpSwipe(Player player) {
		
	}

	public void HandleDownSwipe(Player player) {
		
	}

//	Anchorable anchorable;
//	
//	if (player.IsFalling()) {
//		if (anchorableFinder.FindAnchorableInDirection(out anchorable, swipeDirection)) ConnectGrapplerIfReady(anchorable);
//		else grappler.Misfire(swipeDirection);
//	}
//	else if (player.IsGrappling()) {
//		DisconnectGrapplerIfReady();
//	}
//
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
