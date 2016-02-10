using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	public static ScreenShaker instance;

	[SerializeField] private Camera cam;
	[SerializeField] private float timeBetweenShakes = 0.05f;
	[SerializeField] private FloatRange shakeAmountRange = new FloatRange(0.5f, 2.0f);
	[SerializeField] private FloatRange shakeDurationRange = new FloatRange(0.3f, 1.0f);
	[SerializeField] private FloatRange speedRange = new FloatRange(10.0f, 50.0f);

	private float currentInitialShakeAmount;
	private float currentInitialShakeDuration;
	private float reductionRate;
	private float shakeTimeLeft = 0.0f;
	private float timeOfLastShake = 0;
	
	public void ShakeMin() {
		Shake(shakeAmountRange.min, shakeDurationRange.min);
	}

	public void ShakeMiddle() {
		Shake(shakeAmountRange.middle, shakeDurationRange.middle);
	}

	public void ShakeMax() {
		Shake(shakeAmountRange.max, shakeDurationRange.max);
	}

	public void ShakeLerp(float lerp) {
		lerp = Mathf.Clamp01(lerp);
		Shake(shakeAmountRange.Lerp(lerp), shakeDurationRange.Lerp(lerp));
	}

	public void Shake(float shakeAmount, float shakeDuration) {
		currentInitialShakeDuration = shakeDuration;
		currentInitialShakeAmount = shakeAmount;
		shakeTimeLeft = shakeDuration;
	}

	public void CollisionShake(float collisionSpeed, float collisionAngle) {
		float speedPercent = SpeedToPercent(collisionSpeed);
		float anglePercent = AngleToPercent(collisionAngle);
		float lerp = speedPercent * anglePercent;
		ShakeLerp(lerp);
	}

	private float AngleToPercent(float angle) {
		angle = Mathf.Abs(angle);
		angle = Mathf.Clamp(angle, 0.0f, 180.0f);
		float distanceFrom90 = Mathf.Abs(angle - 90.0f);
		float percent = Mathf.Clamp01(1 - (distanceFrom90 / 90.0f));
		percent = percent / 2.0f + 0.5f; // reframe to go from 0.5f to 1.0f;

		return percent;
	}

	private float SpeedToPercent(float speed) {
		speed = speedRange.Clamp(speed);
		float adjustedSpeed = speed - speedRange.min;
		float percent = Mathf.Clamp01(adjustedSpeed / speedRange.difference);
		return percent;
	}

	public void Stop() {
		shakeTimeLeft = 0.0f;
		Done();
	}

	private void Done() {
		cam.transform.localPosition = Vector3.zero;
		cam.transform.localRotation = Quaternion.identity;
	}

	private void Awake() {
		instance = this;
	}

	private void Start() {
	
	}
	
	private void Update() {
		if (ShouldShake()) Shake();

		if (shakeTimeLeft > 0) shakeTimeLeft -= Time.unscaledDeltaTime;
		else shakeTimeLeft = 0;
	}

	private bool ShouldShake() {
		return shakeTimeLeft > 0 && GetTimeSinceLastShake() > timeBetweenShakes;
	}

	private float GetTimeSinceLastShake() {
		return Time.unscaledTime - timeOfLastShake;
	}

	private void Shake() {
		float timeLeftPercent = shakeTimeLeft / currentInitialShakeDuration;
		float shakeAmount = currentInitialShakeAmount * timeLeftPercent;
		timeOfLastShake = Time.time;
		cam.transform.localPosition = Random.insideUnitSphere * shakeAmount * shakeTimeLeft;
	}
}
