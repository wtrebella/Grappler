using UnityEngine;
using System.Collections;

public class Pickup : GeneratableItem {
	[SerializeField] protected PickupEffect effectPrefab;

	public override void HandleSpawned(Generator generator) {
		base.HandleSpawned(generator);
	}

	private void OnTriggerEnter2D(Collider2D collider) {
		if (WhitTools.IsInLayer(collider.gameObject, "Player")) Collect();
	}

	public void Collect() {
		RunEffect();
		RecycleItem();
	}

	private void RunEffect() {
		PickupEffect effect = Instantiate(effectPrefab);
		effect.Run();
	}
}