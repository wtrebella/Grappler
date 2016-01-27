using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	public static ScreenShaker instance;

	[SerializeField] private Camera cam;
	[SerializeField] private FloatRange shakeAmountRange = new FloatRange(0.5f, 2.0f);
	[SerializeField] private FloatRange shakeDurationRange = new FloatRange(0.3f, 1.0f);
	[SerializeField] private FloatRange speedRange = new FloatRange(10.0f, 50.0f);

	private float currentInitialShakeAmount;
	private float currentInitialShakeDuration;
	private float reductionRate;
	private float shakeTimeLeft = 0.0f;
	
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

	private void Shake(float shakeAmount, float shakeDuration) {
		currentInitialShakeDuration = shakeDuration;
		currentInitialShakeAmount = shakeAmount;
		shakeTimeLeft = shakeDuration;
	}

	public void CollisionShake(float collisionSpeed) {
		float lerp = SpeedToPercent(collisionSpeed);
		ShakeLerp(lerp);
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
		if (shakeTimeLeft > 0) {
			float timeLeftPercent = shakeTimeLeft / currentInitialShakeDuration;
			float shakeAmount = currentInitialShakeAmount * timeLeftPercent;
			cam.transform.localPosition = Random.insideUnitSphere * shakeAmount * shakeTimeLeft;
			shakeTimeLeft -= Time.deltaTime;
		}
		else shakeTimeLeft = 0.0f;
	}
}
