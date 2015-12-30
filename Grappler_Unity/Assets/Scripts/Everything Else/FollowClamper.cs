using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class FollowClamper : MonoBehaviour {
	[SerializeField] private Transform objectToClampTo;
	[SerializeField] private FollowUpdateType updateType;
	[SerializeField] private FollowAxisType axisType;

	private Follow follow;

	private void Awake() {
		follow = GetComponent<Follow>();
	}
	
	private void Update() {
		if (updateType == FollowUpdateType.Update) UpdateClamp();
	}

	private void FixedUpdate() {
		if (updateType == FollowUpdateType.FixedUpdate) UpdateClamp();
	}

	private void UpdateClamp() {
		Vector3 clampObjectPosition = objectToClampTo.position;
		if (axisType == FollowAxisType.X) {
			if (clampObjectPosition.x > follow.minX) follow.minX = clampObjectPosition.x;
		}
		else if (axisType == FollowAxisType.Y) {
			if (clampObjectPosition.y > follow.minY) follow.minY = clampObjectPosition.y;
		}
		else if (axisType == FollowAxisType.XandY) {
			if (clampObjectPosition.x > follow.minX) follow.minX = clampObjectPosition.x;
			if (clampObjectPosition.y > follow.minY) follow.minY = clampObjectPosition.y;
		}
	}
}
