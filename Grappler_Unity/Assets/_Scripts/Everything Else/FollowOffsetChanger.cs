using UnityEngine;
using System.Collections;

public enum FollowOffsetAxes {
	XY,
	X,
	Y
}

public enum FollowOffsetUpdateType {
	Update,
	FixedUpdate
}

public class FollowOffsetChanger : MonoBehaviour {
	[SerializeField] private Follow follow;
	[SerializeField] private FollowOffsetAxes axes = FollowOffsetAxes.XY;
	[SerializeField] private FollowOffsetUpdateType updateType = FollowOffsetUpdateType.Update;
	[SerializeField] private float offsetChangeRate = 1.0f;

	private void Update() {
		IncreaseOffset();
	}

	private void FixedUpdate() {
		IncreaseOffset();
	}

	private void IncreaseOffset() {
		if (updateType == FollowOffsetUpdateType.Update) IncreaseOffset(Time.deltaTime);
		else if (updateType == FollowOffsetUpdateType.FixedUpdate) IncreaseOffset(Time.fixedDeltaTime);
	}

	private void IncreaseOffset(float deltaTime) {
		Vector2 currentOffset = follow.GetOffset();
		Vector2 newOffset = new Vector2(currentOffset.x + offsetChangeRate * deltaTime, currentOffset.y + offsetChangeRate * deltaTime);
		SetOffset(newOffset);
	}

	private void SetOffset(Vector2 offset) {
		if (axes == FollowOffsetAxes.XY) follow.SetOffset(offset);
		else if (axes == FollowOffsetAxes.X) follow.SetOffsetX(offset.x);
		else if (axes == FollowOffsetAxes.Y) follow.SetOffsetY(offset.y);
	}
}
