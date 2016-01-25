using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour {
	public static ScreenShaker instance;

	[SerializeField] private Camera cam;
	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float shakeDuration = 0.3f;
	[SerializeField] private float shakeTimeLeft = 0.0f;
	[SerializeField] private float decreaseFactor = 1.0f;
	
	public void Shake() {
		shakeTimeLeft = shakeDuration;
	}

	public void Stop() {
		shakeTimeLeft = 0;
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
			cam.transform.localPosition = Random.insideUnitSphere * shakeAmount * shakeTimeLeft;
			shakeTimeLeft -= Time.deltaTime;
		}
		else shakeTimeLeft = 0.0f;
	}
}
