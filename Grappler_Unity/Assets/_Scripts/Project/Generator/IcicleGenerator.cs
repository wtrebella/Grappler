using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : ItemOnMountainGenerator {
	private void Awake() {
		base.BaseAwake();
	}

	protected override void HandleMountainChunkGenerated(MountainChunk chunk) {
		int num = chunk.edgePoints.Count;
		float chanceOfSmallIcicleSpawn = 0.05f;
		float dist = 1f / ((float)num + 1f);
		float distVar = dist / 3f;
		for (int i = 1; i <= num; i++) {
			float place = dist * i + Random.Range(-distVar, distVar);
			
			if (Random.value < chanceOfSmallIcicleSpawn) CreateSmallIcicleChunk(chunk, place);
		}
	}
	
	private void CreateSmallIcicleChunk(MountainChunk chunk, float place) {
		float chunkSize = 0.1f;
		float placeVar = 0.05f;
		int num = Random.Range(1, 5);
		float dist = chunkSize / (num + 1);
		float initialSpot = place - chunkSize / 2;
		for (int i = 1; i <= num; i++) {
			float iciclePlace = initialSpot + i * dist + Random.Range(-placeVar, placeVar);
			GeneratableItem item = GenerateItemOnMountainChunk(chunk, iciclePlace);
			item.transform.localScale = new Vector3(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
		}
	}
}
