using UnityEngine;
using System.Collections;

public class Cooldown : MonoBehaviour {
	[SerializeField] private float duration = 1;

	private float timer = 0;

	public void ResetCooldown() {
		timer = duration;
	}

	public float GetCooldownPercentLeft() {
		return Mathf.Clamp01(timer / duration);
	}

	public float GetCooldownPercentDone() {
		return 1.0f - GetCooldownPercentLeft();
	}

	private void Update() {
		UpdateCooldown();
	}

	private void UpdateCooldown() {
		if (timer <= 0) timer = 0;
		else timer -= Time.deltaTime;
	}
}
