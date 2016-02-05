using UnityEngine;
using System.Collections;

public class ItemOnMountainGenerator : Generator {
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private void Awake() {
		BaseAwake();
	}

	protected override void BaseAwake() {
		mountainChunkGenerator.SignalMountainChunkGenerated += HandleMountainChunkGenerated;
	}

	protected virtual void HandleMountainChunkGenerated(MountainChunk chunk) {
		
	}

	protected void GenerateItemOnMountainChunk(MountainChunk chunk, float place) {
		GeneratableItem item = prefab.Spawn();
		item.transform.parent = chunk.transform;
		Vector3 position = chunk.PlaceToPosition(place);
		position.z += 0.1f;
		item.transform.position = position;
		HandleItemGenerated(item);
	}
}