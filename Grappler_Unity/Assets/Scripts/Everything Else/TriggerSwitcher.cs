using UnityEngine;
using System.Collections;

public class TriggerSwitcher : MonoBehaviour {
	[SerializeField] private Collider2D[] colliders;

	public void SetAsTrigger(float delay = 0) {
		StartCoroutine(SetAsTriggerCoroutine(delay));
	}

	public void SetAsNonTrigger(float delay = 0) {
		StartCoroutine(SetAsNonTriggerCoroutine(delay));
	}

	private IEnumerator SetAsTriggerCoroutine(float delay = 0) {
		yield return new WaitForSeconds(delay);

		foreach (Collider2D c in colliders) c.isTrigger = true;
	}

	private IEnumerator SetAsNonTriggerCoroutine(float delay = 0) {
		yield return new WaitForSeconds(delay);
		
		foreach (Collider2D c in colliders) c.isTrigger = false;
	}
}
