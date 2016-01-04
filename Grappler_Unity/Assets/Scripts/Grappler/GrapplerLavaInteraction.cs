using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class GrapplerLavaInteraction : MonoBehaviour {
	public Action SignalEnteredLava;

	private void OnTriggerEnter2D(Collider2D collider) {
		Lava lava = collider.GetComponent<Lava>();
		if (lava) {
			if (SignalEnteredLava != null) SignalEnteredLava();
		}
	}
}
