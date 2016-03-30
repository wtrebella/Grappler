using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GrapplerManualMover))]
public class GrapplerInitialPlacer : MonoBehaviour {
	[SerializeField] private Vector2 mountainChunkFirstPointOffset = new Vector2(0, -2);
	[SerializeField] private MountainChunkGeneratorOld mountainChunkGenerator;

	private GrapplerManualMover manualMover;

	private void Awake() {
		manualMover = GetComponent<GrapplerManualMover>();
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		if (mountainChunkGenerator.numMountainChunksCreated == 1) Place(mountainChunk);
	}

	private void Place(MountainChunk mountainChunk) {
		Vector3 chunkOrigin = mountainChunk.GetFirstEdgePoint().vector;
		Vector3 playerPosition = chunkOrigin;
		playerPosition.x += mountainChunkFirstPointOffset.x;
		playerPosition.y += mountainChunkFirstPointOffset.y;

		manualMover.InstanlyMoveTo(playerPosition);
	}
}
