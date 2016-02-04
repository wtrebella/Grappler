using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CameraMover))]
public class CameraMoverHorizontalSmoothDampAdjuster : MonoBehaviour {
	[SerializeField] private FloatRange smoothDampRange = new FloatRange(0.05f, 0.3f);
	[SerializeField] private Rigidbody2D speedingObject;
	[SerializeField] private float speedHighEnd = 60.0f;
	[SerializeField] private float smoothDampTime = 0.3f;

	private float smoothDampVelocity;

	private CameraMover cameraMover;

	private void Awake() {
		cameraMover = GetComponent<CameraMover>();
	}
	private float max = 0;
	private void Update() {
		UpdateSmoothDamp();

		float speed = speedingObject.velocity.magnitude;
		if (speed > max) max = speed;
	}

	private void UpdateSmoothDamp() {
		float speed = GetSpeed();
		float smoothDamp = GetSmoothedSmoothDamp(speed);
		cameraMover.SetSmoothDampTimeX(smoothDamp);
	}

	private float GetSpeed() {
		return speedingObject.velocity.magnitude;
	}

	private float SpeedToSmoothDamp(float speed) {
		float percent = Mathf.Clamp01(speed / speedHighEnd);
		float totalSmoothDamp = smoothDampRange.max - smoothDampRange.min;
		float smoothDampPiece = percent * totalSmoothDamp;
		float convertedSmoothDamp = smoothDampRange.min + smoothDampPiece;
		return convertedSmoothDamp;
	}

	private float GetSmoothedSmoothDamp(float speed) {
		float targetSmoothDamp = SpeedToSmoothDamp(speed);
		float smoothedSmoothDamp = Mathf.SmoothDamp(cameraMover.GetSmoothDampTime().x, targetSmoothDamp, ref smoothDampVelocity, smoothDampTime);
		return smoothedSmoothDamp;
	}
}
