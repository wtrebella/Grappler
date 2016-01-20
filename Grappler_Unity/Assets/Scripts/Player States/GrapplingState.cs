using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(GrapplerRope))]
public class GrapplingState : MonoBehaviour {
	[SerializeField] private Player player;
	[SerializeField] private float cooldown = 0.5f;

	private bool isReady = true;
	private float cooldownTimer;
	private GrapplerRope grapplerRope;

	public float GetCooldownCompletionPercentage() {
		if (ReadyToConnect()) return 1;
		else {
			float percentage = Mathf.Clamp01(1 - (cooldownTimer / cooldown));
			return percentage;
		}
	}

	private void Awake() {
		grapplerRope = GetComponent<GrapplerRope>();
		cooldownTimer = cooldown;
	}

	public void Misfire(Vector2 direction) {
		grapplerRope.Misfire(direction);
	}

	public bool ReadyToConnect() {
		return isReady && grapplerRope.IsRetracted() && !grapplerRope.IsConnected();
	}

	public bool ReadyToDisconnect() {
		return grapplerRope.IsConnected();
	}

	public void Connect(Anchorable anchorable) {
		grapplerRope.Connect(anchorable);
	}

	public void ReleaseGrapple() {
		grapplerRope.Release();
		StartCoroutine(Cooldown());
	}

	private IEnumerator Cooldown() {
		isReady = false;
		cooldownTimer = cooldown;
		while (cooldownTimer > 0) {
			cooldownTimer -= Time.deltaTime;
			yield return null;
		}
		cooldownTimer = cooldown;
		isReady = true;
	}
}
