using UnityEngine;
using System.Collections;

public class Coin : GeneratableItem {
	private CoinGenerator coinGenerator;

	public override void HandleSpawned(Generator generator) {
		base.HandleSpawned(generator);
		coinGenerator = (CoinGenerator)parentGenerator;
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, "Player")) Collect();
	}

	public void Collect() {
		coinGenerator.OnChildCoinCollected();
		RecycleItem();
	}
}
