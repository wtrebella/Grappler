using UnityEngine;
using System.Collections;

public class RockSlide : MonoBehaviour {
	[SerializeField] private Follow follow;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private FloatRange offsetChangeRateRange = new FloatRange(-4, 4);
	[SerializeField] private float pushBackAmount = -10;
	[SerializeField] private float streakThreshold = 0.3f;

	private float invertedSpeedPercent = 1;
	private float currentOffsetChangeRate = 0;

	public void OnAirTimeStreakEndedByCollision(float streak) {

	}

	public void OnAirTimeStreakEndedByGrapple(float streak) {
		if (streak < streakThreshold) return;
		PushBack(streak);
	}

	public void OnSpeedPercentChanged(float percent) {
		invertedSpeedPercent = 1 - percent;
	}

	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		UpdateOffsetUpdate();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		UpdateOffsetFixedUpdate();
	}

	private void PushBack(float streak) {
		Vector2 currentOffset = follow.GetOffset();
		currentOffset.x += pushBackAmount * streak;
		SetOffset(currentOffset.x);
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
		float newOffsetX = currentOffset.x + currentOffsetChangeRate * deltaTime;
		SetOffset(newOffsetX);
	}

	private void SetOffset(float offsetX) {
		follow.SetOffsetX(Mathf.Max(0, offsetX));
	}
}
