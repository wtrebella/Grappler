using UnityEngine;
using System.Collections;

public class GrapplerCoinCollector : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D collider) {
		Coin coin = collider.GetComponent<Coin>();
		if (coin) coin.Collect();
	}
}
