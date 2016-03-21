using UnityEngine;
using System.Collections;

public class GrapplingManager : MonoBehaviour {
	public float GetCooldownCompletionPercentage() {
		if (ReadyToConnect()) return 1;
		else {
			float percentage = Mathf.Clamp01(1 - (cooldownTimer / cooldown));
			return percentage;
		}
	}

	[SerializeField] private float cooldown = 0.5f;
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

	private bool isInCooldown = false;
	private float cooldownTimer;

	private void Awake() {
		cooldownTimer = cooldown;
	}

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

		Connect(anchorable);
		return true;
	}

	private void ReleaseGrapple() {
		grapplerRope.Release();
		StartCoroutine("Cooldown");
	}

	private void Connect(Anchorable anchorable) {
		grapplerRope.Connect(anchorable);
	}

	private bool ReadyToConnect() {
		return !isInCooldown && grapplerRope.IsRetracted() && !grapplerRope.IsConnected();
	}

	private bool ReadyToDisconnect() {
		return grapplerRope.IsConnected();
	}


	private void FinishCooldown() {
		StopCoroutine("Cooldown");
		isInCooldown = false;
		cooldownTimer = cooldown;
	}

	private IEnumerator Cooldown() {
		isInCooldown = true;
		cooldownTimer = cooldown;
		while (cooldownTimer > 0) {
			cooldownTimer -= Time.deltaTime;
			yield return null;
		}
		cooldownTimer = cooldown;
		isInCooldown = false;
	}
}