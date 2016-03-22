using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// TODO: get rid of hasStarted and extract it out into game manager

public class RockSlide : MonoBehaviour {
	public UnityEventWithFloat OnPushBack;

	[SerializeField] private Player player;
	[SerializeField] private Transform startPoint;
	[SerializeField] private Follow follow;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private FloatRange offsetChangeRateRange = new FloatRange(-4, 4);
	[SerializeField] private Transform playerBody;
	[SerializeField] private float slowZoneDistanceFromPlayer = 10;
	[SerializeField] private float slowZoneMultiplier = 0.1f;
	[SerializeField] private float pushBackAmount = -10;
	[SerializeField] private float streakThreshold = 0.3f;

	private bool hasStarted = false;

	private float invertedSpeedPercent = 1;
	private float currentOffsetChangeRate = 0;

	public float GetStreakThreshold() {
		return streakThreshold;
	}

	public void StopMoving() {
		follow.enabled = false;
	}

	public void OnAirTimeStreakEndedByCollision(float streak) {

	}

	public void OnGrapple() {
//		float pushBackPercent = 1 - player.groundDetector.GetDistanceFromGroundPercent();
//		PushBack(pushBackPercent);
	}

	public void OnAirTimeStreakEndedByGrapple(float streak) {
		if (streak < streakThreshold) return;
		PushBack(streak);
	}

	public void OnSpeedPercentChanged(float percent) {
		invertedSpeedPercent = 1 - percent;
	}

	private float GetDistanceToPlayer() {
		return Mathf.Abs(startPoint.position.x - playerBody.position.x);
	}

	private bool IsInSlowZone() {
		return GetDistanceToPlayer() < slowZoneDistanceFromPlayer;
	}

	private void Update() {
		if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) hasStarted = true;

		if (updateType != WhitUpdateType.Update) return;
		if (!hasStarted) return;
		UpdateOffsetUpdate();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;
		if (!hasStarted) return;
		UpdateOffsetFixedUpdate();
	}

	private void PushBack(float amount) {
		float pushBack = pushBackAmount * amount;
		Vector2 currentOffset = follow.GetOffset();
		currentOffset.x += pushBack;
		SetOffset(currentOffset.x);
		if (OnPushBack != null) OnPushBack.Invoke(amount);
	}
	
	private void UpdateOffsetUpdate() {
		UpdateOffset(Time.deltaTime * Time.timeScale);
	}

	private void UpdateOffsetFixedUpdate() {
		UpdateOffset(Time.fixedDeltaTime * Time.timeScale);
	}

	private void UpdateOffset(float deltaTime) {
		currentOffsetChangeRate = offsetChangeRateRange.Lerp(invertedSpeedPercent);
		Vector2 currentOffset = follow.GetOffset();
		float extraOffset = currentOffsetChangeRate * deltaTime;
		if (IsInSlowZone()) extraOffset *= slowZoneMultiplier;
		float newOffsetX = currentOffset.x + extraOffset;

		SetOffset(newOffsetX);
	}

	private void SetOffset(float offsetX) {
		follow.SetOffsetX(Mathf.Max(0, offsetX));
	}
}
