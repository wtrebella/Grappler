using UnityEngine;
using System.Collections;

public class TemporaryTriggerSetter : MonoBehaviour {
	[SerializeField] private Collider2D[] colliders;

	public void SetToTriggerTemporarily(float duration) {
		StartCoroutine(SetToTriggerForDuration(duration));
	}

	private void SetToTrigger() {
		foreach (Collider2D collider in colliders) collider.isTrigger = true;
	}

	private void SetToNormal() {
		foreach (Collider2D collider in colliders) collider.isTrigger = false;
	}

	private IEnumerator SetToTriggerForDuration(float duration) {
		SetToTrigger();
		yield return new WaitForSeconds(duration);
		SetToNormal();
	}
}
