using UnityEngine;
using System.Collections;

// TODO make it so you can slightly outrun the avalanche

[RequireComponent(typeof(FollowOffsetChanger))]
public class FollowOffsetChangerSpeedBased : MonoBehaviour {
	[SerializeField] private Rigidbody2D rigid;
	[SerializeField] private float speedCutoff = 40;

	private FollowOffsetChanger offsetChanger;

	private void Awake() {
		offsetChanger = GetComponent<FollowOffsetChanger>();
	}

	private void FixedUpdate() {
		UpdateOffsetChangerState();
	}

	private void UpdateOffsetChangerState() {
		if (ShouldTurnOffsetChangerOn()) TurnOffsetChangerOnIfOff();
		else TurnOffsetChangerOffIfOn();
	}

	private bool IsOverSpeedCutoff() {
		return GetSpeed() > speedCutoff;
	}

	private void TurnOffsetChangerOnIfOff() {
		if (offsetChanger.IsOff()) offsetChanger.TurnOn();
	}

	private void TurnOffsetChangerOffIfOn() {
		if (offsetChanger.IsOn()) offsetChanger.TurnOff();
	}

	private bool ShouldTurnOffsetChangerOn() {
		return !IsOverSpeedCutoff();
	}

	private void TurnOffsetChangerOn() {
		offsetChanger.TurnOn();
	}

	private void TurnOffsetChangerOff() {
		offsetChanger.TurnOff();
	}

	private float GetSpeed() {
		return rigid.velocity.magnitude;
	}
}
