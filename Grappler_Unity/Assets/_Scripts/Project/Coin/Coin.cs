using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	private CoinPattern coinPattern;

	private void OnTriggerEnter2D(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, "Player")) Collect();
	}

	public void SetCoinPattern(CoinPattern coinPattern) {
		this.coinPattern = coinPattern;
	}

	public void Collect() {
		this.Recycle();
	}
}