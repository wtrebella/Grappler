using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, "Player")) Collect();
	}

	public void Collect() {
		this.Recycle();
	}

	private void Update() {
		transform.eulerAngles = Vector3.zero;
	}
}