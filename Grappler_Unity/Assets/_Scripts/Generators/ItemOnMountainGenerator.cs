using UnityEngine;
using System.Collections;

public class ItemOnMountainGenerator : Generator {
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private void Awake() {
		base.BaseAwake();
	}

	protected override void BaseAwake() {
		base.BaseAwake();
		mountainChunkGenerator.SignalMountainChunkGenerated += HandleMountainChunkGenerated;
	}

	protected virtual void HandleMountainChunkGenerated(MountainChunk chunk) {
		
	}

	protected GeneratableItem GenerateItemOnMountainChunk(MountainChunk chunk, float place) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = chunk.transform;
		Vector3 position = chunk.PlaceToPosition(place);
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}
}