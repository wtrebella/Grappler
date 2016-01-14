using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(GrapplerRope))]
public class Grappling : MonoBehaviour {
	[SerializeField] private Player player;

	private GrapplerRope grapplerRope;

	private void Awake() {
		grapplerRope = GetComponent<GrapplerRope>();
	}

	public void Misfire(Vector2 direction) {
		grapplerRope.Misfire(direction);
	}

	public bool ReadyToConnect() {
		return grapplerRope.IsRetracted() && !grapplerRope.IsConnected();
	}

	public bool ReadyToDisconnect() {
		return grapplerRope.IsConnected();
	}

	public void Connect(Anchorable anchorable) {
		grapplerRope.Connect(anchorable);
	}

	public void ReleaseGrapple() {
		grapplerRope.Release();
	}

	public void ClimbUp() {

	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) ClimbUp();
	}
}
