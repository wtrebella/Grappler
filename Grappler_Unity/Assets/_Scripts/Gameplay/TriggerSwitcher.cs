using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerSwitcher : MonoBehaviour {
	private List<Collider2D> colliders;

	private void Awake() {
		colliders = new List<Collider2D>();
	}

	public void AddCollider(Collider2D coll) {
		colliders.Add(coll);
	}

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
