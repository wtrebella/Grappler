using UnityEngine;
using System.Collections;

public class Coin : GeneratableItem {
	[SerializeField] private Transform body;

	public override void HandleSpawned(Generator generator) {
		base.HandleSpawned(generator);

	}

	public void Collect() {
		RecycleItem();
	}

	private void FixedUpdate() {
		
	}
}
