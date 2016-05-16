using UnityEngine;
using System.Collections;

public class Crag : GeneratableItem {
	[SerializeField] private FixedTimeParticleSystem particles;
	[SerializeField] private MeshRenderer sprite;

	public override void HandleSpawned(Generator generator) {
		base.HandleSpawned(generator);
		sprite.enabled = true;
	}

	public void Explode() {
		sprite.enabled = false;
		particles.Restart();
		StartCoroutine(WaitThenRecycle());
	}

	private IEnumerator WaitThenRecycle() {
		yield return new WaitForSeconds(5);
		RecycleItem();
	}
}