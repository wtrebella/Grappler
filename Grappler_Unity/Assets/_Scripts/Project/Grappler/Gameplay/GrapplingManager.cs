using UnityEngine;
using System.Collections;
using System;
using WhitTerrain;

public class GrapplingManager : MonoBehaviour {
	public static GrapplingManager instance;

	public Action<bool> SignalGrapplingEnabledChanged;

	public Vector2 targetDirection {get; private set;}

	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private AnchorableFinder anchorableFinder;
	[SerializeField] private GrapplerRope grapplerRope;
	[SerializeField] private Vector2 grappleBoost = new Vector2(200, -1000);

	private Player _player;
	protected Player player {
		get {
			if (_player == null) _player = GetComponentInParent<Player>();
			if (_player == null) Debug.LogError("must be child of Player");
			return _player;
		}
	}

	private bool hasConnected = false;
	private bool grapplingIsEnabled = true;

	public void EnableGrappling() {
		grapplingIsEnabled = true;
		RunGrapplingEnabledChangedSignal();
	}

	public void DisableGrappling() {
		grapplingIsEnabled = false;
		RunGrapplingEnabledChangedSignal();
	}

	private void RunGrapplingEnabledChangedSignal() {
		if (SignalGrapplingEnabledChanged != null) SignalGrapplingEnabledChanged(grapplingIsEnabled);	
	}

	public void SetTargetDirection(Vector2 targetDirection) {
		this.targetDirection = targetDirection;
	}

	private void Awake() {
		targetDirection = Vector2.zero;
		instance = this;
	}

	public Vector2 GetGrapplePoint() {
		return grapplerRope.GetEndPoint();
	}

	public bool Connect() {
		bool connected = ConnectGrapplerToHighestAnchorable();
		return connected;
	}

	public bool Disconnect() {
		bool disconnected = DisconnectGrapplerIfPossible();
		return disconnected;
	}

	private bool ConnectGrapplerToHighestAnchorable() {
		if (!ReadyToConnect()) return false;
		Anchorable anchorable;
//		if (anchorableFinder.FindAnchorableInScreenOverlap(out anchorable)) {
//			return ConnectGrapplerIfPossible(anchorable);
//		}
		if (anchorableFinder.FindAnchorableInArc(out anchorable)) {
			return ConnectGrapplerIfPossible(anchorable);
		}
		return false;
	}

	private bool DisconnectGrapplerIfPossible() {
		if (!ReadyToDisconnect()) return false;

		ReleaseGrapple();
		return true;
	}

	private bool ConnectGrapplerIfPossible(Anchorable anchorable) {
		if (!ReadyToConnect()) return false;

		if (!hasConnected) {
			grapplerRope.ResetAccelerationCooldown();
			hasConnected = true;
		}

		Connect(anchorable);

		return true;
	}

	private void ReleaseGrapple() {
		grapplerRope.Release();
	}

	private void Connect(Anchorable anchorable) {
		grapplerRope.Connect(anchorable);
	}

	private bool ReadyToConnect() {
		return grapplingIsEnabled && 
			grapplerRope.IsRetracted() && 
			!grapplerRope.IsConnected();
	}

	private bool ReadyToDisconnect() {
		return grapplerRope.IsConnected();
	}

	private void FixedUpdate() {
		if (grapplerRope.IsConnected()) ApplyGrappleBoost();
	}

	private void ApplyGrappleBoost() {
		player.rigidbodyAffecterGroup.AddForce(grappleBoost, ForceMode2D.Force);
	}
}