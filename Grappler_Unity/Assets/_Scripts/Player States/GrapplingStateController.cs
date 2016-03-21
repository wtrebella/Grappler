using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Events;

public class GrapplingStateController : PlayerStateController {
	public float GetCooldownCompletionPercentage() {
		if (ReadyToConnect()) return 1;
		else {
			float percentage = Mathf.Clamp01(1 - (cooldownTimer / cooldown));
			return percentage;
		}
	}

	[SerializeField] private float cooldown = 0.5f;

	[SerializeField] private Rigidbody2DVelocityReducer velocityReducer;
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private AirTimeTimer airTimeTimer;
	[SerializeField] private AnchorableFinder anchorableFinder;

	private bool isReady = true;
	private float cooldownTimer;
	private GrapplerRope grapplerRope;
	
	private void Awake() {
		base.BaseAwake();
		state = Player.PlayerStates.Grappling;
		grapplerRope = GetComponent<GrapplerRope>();
		cooldownTimer = cooldown;
	}

	public void ConnectGrapplerToHighestAnchorable() {
		if (!ReadyToConnect()) return;
		Anchorable anchorable;
		if (anchorableFinder.FindAnchorableInScreenOverlap(out anchorable)) {
			ConnectGrapplerIfPossible(anchorable);
		}
	}
	
	public bool DisconnectGrapplerIfPossible() {
		if (ReadyToDisconnect()) {
			ReleaseGrapple();
			return true;
		}
		else return false;
	}
	
	public bool ConnectGrapplerIfPossible(Anchorable anchorable) {
		if (ReadyToConnect()) {
			Connect(anchorable);
			player.SetState(Player.PlayerStates.Grappling);
			return true;
		}

		return false;
	}

	public override void EnterState() {
		base.EnterState();
		airTimeTimer.OnGrapple();
		velocityReducer.Reduce();
		rockSlide.OnGrapple();
		player.kinematicSwitcher.SetNonKinematic();
		player.playerAnimator.PlayGrapplingAnimations();
	}
	
	public override void TouchUp() {
		base.TouchUp();
		if (DisconnectGrapplerIfPossible()) {
			player.SetState(Player.PlayerStates.Falling);
		}
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
		StartCoroutine("Cooldown");
	}

	public void FinishCooldown() {
		StopCoroutine("Cooldown");
		isReady = true;
		cooldownTimer = cooldown;
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
