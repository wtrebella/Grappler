using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// TODO: get rid of hasStarted and extract it out into game manager

public class RockSlide : MonoBehaviour {
	public UnityEventWithFloat OnPushBack;

	[SerializeField] private Transform startPoint;
	[SerializeField] private Follow follow;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private FloatRange offsetChangeRateRange = new FloatRange(-4, 4);
	[SerializeField] private Transform playerBody;
	[SerializeField] private float slowZoneDistanceFromPlayer = 10;
	[SerializeField] private float slowZoneMultiplier = 0.1f;
	[SerializeField] private float pushBackAmount = -10;

	private bool hasStarted = false;

	private float invertedSpeedPercent = 1;
	private float currentOffsetChangeRate = 0;

	public void StopMoving() {
		follow.enabled = false;
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
		if (InputManager.instance.GetMouseButtonDown(0) || InputManager.instance.GetTouchBegan()) hasStarted = true;

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
		float deltaTime = Time.deltaTime * Time.timeScale;
//		UpdateOffset(deltaTime);
		Move(deltaTime);
	}

	private void UpdateOffsetFixedUpdate() {
		float deltaTime = Time.fixedDeltaTime * Time.timeScale;
//		UpdateOffset(deltaTime);
		Move(deltaTime);
	}

	private void Move(float deltaTime) {
		float speed;
		if (IsInSlowZone()) speed = 40;
		else speed = 80;
		if (IsInSlowZone()) Debug.Log("slow zone");
		else Debug.Log("fast zone");
		Vector3 position = transform.position;
		position.x += speed * deltaTime;
		transform.position = position;
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
