using UnityEngine;
using System.Collections;

public class FollowOffsetChanger : MonoBehaviour {
	[SerializeField] private Follow follow;
	[SerializeField] private WhitAxisType axes = WhitAxisType.XY;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private float offsetChangeRate = 1.0f;

	private bool isOn = false;

	public void TurnOn() {
		isOn = true;
	}

	public void TurnOff() {
		isOn = false;
	}

	public bool IsOn() {
		return isOn;
	}

	public bool IsOff() {
		return !isOn;
	}

	private void Update() {
		if (isOn) IncreaseOffset();
	}

	private void FixedUpdate() {
		if (isOn) IncreaseOffset();
	}

	private void IncreaseOffset() {
		if (updateType == WhitUpdateType.Update) IncreaseOffset(Time.deltaTime * Time.timeScale);
		else if (updateType == WhitUpdateType.FixedUpdate) IncreaseOffset(Time.fixedDeltaTime * Time.timeScale);
	}

	private void IncreaseOffset(float deltaTime) {
		Vector2 currentOffset = follow.GetOffset();
		Vector2 newOffset = new Vector2(currentOffset.x + offsetChangeRate * deltaTime, currentOffset.y + offsetChangeRate * deltaTime);
		SetOffset(newOffset);
	}

	private void SetOffset(Vector2 offset) {
		if (axes == WhitAxisType.XY) follow.SetOffset(offset);
		else if (axes == WhitAxisType.X) follow.SetOffsetX(offset.x);
		else if (axes == WhitAxisType.Y) follow.SetOffsetY(offset.y);
	}
}
