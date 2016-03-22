using UnityEngine;
using System.Collections;

public class GrapplingManager : MonoBehaviour {
	[SerializeField] private RockSlide rockSlide;
	[SerializeField] private AirTimeTimer airTimeTimer;
	[SerializeField] private AnchorableFinder anchorableFinder;
	[SerializeField] private GrapplerRope grapplerRope;

	private Player _player;
	protected Player player {
		get {
			if (_player == null) _player = GetComponentInParent<Player>();
			if (_player == null) Debug.LogError("must be child of Player");
			return _player;
		}
	}

	private bool hasConnected = false;

	public bool Connect() {
		return ConnectGrapplerToHighestAnchorable();
	}

	public bool Disconnect() {
		return DisconnectGrapplerIfPossible();
	}

	private bool ConnectGrapplerToHighestAnchorable() {
		if (!ReadyToConnect()) return false;
		Anchorable anchorable;
		if (anchorableFinder.FindAnchorableInScreenOverlap(out anchorable)) {
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
		return grapplerRope.IsRetracted() && !grapplerRope.IsConnected();
	}

	private bool ReadyToDisconnect() {
		return grapplerRope.IsConnected();
	}
}