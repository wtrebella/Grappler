using UnityEngine;
using System.Collections;

public enum OffsetChangerState {
	NotMoving,
	MovingForward,
	MovingBackwards
}

public class FollowOffsetChanger : MonoBehaviour {
	[SerializeField] private Follow follow;
	[SerializeField] private WhitAxisType axes = WhitAxisType.XY;
	[SerializeField] private WhitUpdateType updateType = WhitUpdateType.Update;
	[SerializeField] private float offsetChangeRate = 1.0f;

	private OffsetChangerState state = OffsetChangerState.NotMoving;

	public void SetToMovingForward() {
		state = OffsetChangerState.MovingForward;
	}

	public void SetToMovingBackwards() {
		state = OffsetChangerState.MovingBackwards;
	}

	public void SetToNotMoving() {
		state = OffsetChangerState.NotMoving;
	}

	public bool IsMovingForward() {
		return state == OffsetChangerState.MovingForward;
	}

	public bool IsMovingBackwards() {
		return state == OffsetChangerState.MovingBackwards;
	}

	public bool IsNotMoving() {
		return state == OffsetChangerState.NotMoving;
	}

	private void Update() {
		if (updateType != WhitUpdateType.Update) return;

		UpdateOffset();
	}

	private void FixedUpdate() {
		if (updateType != WhitUpdateType.FixedUpdate) return;

		FixedUpdateOffset();
	}

	private void UpdateOffset() {
		if (state == OffsetChangerState.MovingForward) IncreaseOffsetUpdate();
		else if (state == OffsetChangerState.MovingBackwards) DecreaseOffsetUpdate();
	}

	private void FixedUpdateOffset() {
		if (state == OffsetChangerState.MovingForward) IncreaseOffsetFixedUpdate();
		else if (state == OffsetChangerState.MovingBackwards) DecreaseOffsetFixedUpdate();
	}

	private void IncreaseOffsetUpdate() {
		IncreaseOffset(Time.deltaTime * Time.timeScale);
	}

	private void IncreaseOffsetFixedUpdate() {
		IncreaseOffset(Time.fixedDeltaTime * Time.timeScale);
	}

	private void DecreaseOffsetUpdate() {
		DecreaseOffset(Time.deltaTime * Time.timeScale);
	}

	private void DecreaseOffsetFixedUpdate() {
		DecreaseOffset(Time.fixedDeltaTime * Time.timeScale);
	}

	private void IncreaseOffset(float deltaTime) {
		Vector2 currentOffset = follow.GetOffset();
		Vector2 newOffset = new Vector2(currentOffset.x + offsetChangeRate * deltaTime, currentOffset.y + offsetChangeRate * deltaTime);
		SetOffset(newOffset);
	}

	private void DecreaseOffset(float deltaTime) {
		Vector2 currentOffset = follow.GetOffset();
		Vector2 newOffset = new Vector2(currentOffset.x - offsetChangeRate * deltaTime, currentOffset.y - offsetChangeRate * deltaTime);
		SetOffset(newOffset);
	}

	private void SetOffset(Vector2 offset) {
		if (axes == WhitAxisType.XY) follow.SetOffset(offset);
		else if (axes == WhitAxisType.X) follow.SetOffsetX(offset.x);
		else if (axes == WhitAxisType.Y) follow.SetOffsetY(offset.y);
	}
}
