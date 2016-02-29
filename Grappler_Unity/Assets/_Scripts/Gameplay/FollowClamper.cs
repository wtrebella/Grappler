using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Follow))]
public class FollowClamper : MonoBehaviour {
	[SerializeField] private Transform objectToClampTo;
	[SerializeField] private WhitUpdateType updateType;
	[SerializeField] private WhitAxisType axisType;

	private Follow follow;

	private void Awake() {
		follow = GetComponent<Follow>();
	}
	
	private void Update() {
		if (updateType == WhitUpdateType.Update) UpdateClamp();
	}

	private void FixedUpdate() {
		if (updateType == WhitUpdateType.FixedUpdate) UpdateClamp();
	}

	private void UpdateClamp() {
		Vector3 clampObjectPosition = objectToClampTo.position;
		if (axisType == WhitAxisType.X) {
			if (clampObjectPosition.x > follow.minX) follow.minX = clampObjectPosition.x;
		}
		else if (axisType == WhitAxisType.Y) {
			if (clampObjectPosition.y > follow.minY) follow.minY = clampObjectPosition.y;
		}
		else if (axisType == WhitAxisType.XY) {
			if (clampObjectPosition.x > follow.minX) follow.minX = clampObjectPosition.x;
			if (clampObjectPosition.y > follow.minY) follow.minY = clampObjectPosition.y;
		}
	}
}
