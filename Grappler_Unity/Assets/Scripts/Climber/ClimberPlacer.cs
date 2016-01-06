using UnityEngine;
using System.Collections;

public class ClimberPlacer : MonoBehaviour {
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private float placeOnChunk = 0;
	private MountainChunk chunk;

	private void Awake() {
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		if (chunk != null) return;
		chunk = mountainChunk;
	}

	private void PlaceOnMountainChunk(MountainChunk mountainChunk, float place) {
		if (place == placeOnChunk) return;
		placeOnChunk = place;
		transform.position = mountainChunk.GetPositionAlongLine(placeOnChunk);
	}

	private void Update() {
		if (chunk == null) return;

		PlaceOnMountainChunk(chunk, placeOnChunk + 0.01f * Time.deltaTime);
	}
}
