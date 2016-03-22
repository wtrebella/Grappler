using UnityEngine;
using System.Collections;

public class SpringJointAttributeCooldownLerper : MonoBehaviour {
	[SerializeField] private SpringJointAttributes fullCooldownAttributes;
	[SerializeField] private SpringJointAttributes noCooldownAttributes;
	[SerializeField] private Cooldown cooldown;

	public float GetDistance() {
		return Mathf.Lerp(noCooldownAttributes.distance, fullCooldownAttributes.distance, cooldown.GetCooldownPercentLeft());
	}

	public float GetFrequency() {
		return Mathf.Lerp(noCooldownAttributes.frequency, fullCooldownAttributes.frequency, cooldown.GetCooldownPercentLeft());
	}

	public float GetDampingRatio() {
		return Mathf.Lerp(noCooldownAttributes.dampingRatio, fullCooldownAttributes.dampingRatio, cooldown.GetCooldownPercentLeft());
	}
}
