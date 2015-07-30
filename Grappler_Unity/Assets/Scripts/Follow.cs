using UnityEngine;
using System.Collections;

public enum Updatetype {
	Update,
	FixedUpdate
}

public enum FollowType {
	Smoothdamp,
	Immediate
}

public class Follow : MonoBehaviour {
	[SerializeField] Transform objectToFollow;
	[SerializeField] Updatetype updateType;
	[SerializeField] FollowType followType;

	private float smoothDampTime = 0.15f;
	private Vector3 smoothDampVelocity;

	private void Update() {
		if (updateType == Updatetype.Update) UpdateMovement();
	}

	private void FixedUpdate() {
		if (updateType == Updatetype.FixedUpdate) UpdateMovement();
	}

	private void UpdateMovement() {
		if (followType == FollowType.Smoothdamp) UpdateMovementSmoothDamp();
		else if (followType == FollowType.Immediate) UpdateMovementImmediate();
	}

	private void UpdateMovementSmoothDamp() {
		Vector2 objectPosition = objectToFollow.position.ToVector2();
		Vector3 cameraCurrentPosition = transform.position;
		Vector3 cameraTargetPosition = objectPosition.ToVector3(cameraCurrentPosition.z);
		Vector3 cameraSmoothedPosition = Vector3.SmoothDamp(cameraCurrentPosition, cameraTargetPosition, ref smoothDampVelocity, smoothDampTime);
		transform.position = cameraSmoothedPosition;
	}

	private void UpdateMovementImmediate() {
		Vector2 objectPosition = objectToFollow.position.ToVector2();
		Vector3 cameraCurrentPosition = transform.position;
		Vector3 cameraTargetPosition = objectPosition.ToVector3(cameraCurrentPosition.z);
		transform.position = cameraTargetPosition;
	}
}
