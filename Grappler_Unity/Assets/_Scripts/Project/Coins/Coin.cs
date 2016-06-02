using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	[SerializeField] private CoinMagnetism magnetism;

	private void OnTriggerEnter2D(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, "Player")) Collect();
	}

	public void Collect() {
		GameStats.instance.OnCoinCollected();
		this.Recycle();
	}

	public void Reset() {
		magnetism.Demagnetize();
	}
}