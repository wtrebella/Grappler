using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : MonoBehaviour {
	[SerializeField] private Icicle iciclePrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private List<Icicle> icicles;
	
	private void Awake() {
		icicles = new List<Icicle>();
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk chunk) {
		int num = 20;
		float chanceOfSpawn = 0.4f;
		float dist = 1f / ((float)num + 1f);
		float distVar = dist / 3f;
		for (int i = 1; i <= num; i++) {
			float place = dist * i + Random.Range(-distVar, distVar);
			
			if (Random.value < chanceOfSpawn) CreateIcicleChunk(chunk, place);
		}
	}

	private void CreateIcicleChunk(MountainChunk chunk, float place) {
		float chunkSize = 0.04f;
		float placeVar = 0.01f;
		int num = Random.Range(1, 5);
		float dist = chunkSize / (num + 1);
		float initialSpot = place - chunkSize / 2;
		for (int i = 1; i <= num; i++) {
			float iciclePlace = initialSpot + i * dist + Random.Range(-placeVar, placeVar);
			CreateIcicle(chunk, iciclePlace);
		}
	}
	
	private void CreateIcicle(MountainChunk chunk, float place) {
		Icicle icicle = iciclePrefab.Spawn();
		icicle.transform.localScale = new Vector3(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
		icicle.transform.parent = chunk.transform;
		Vector3 position = chunk.GetPositionAtPlace(place);;
		position.z += 0.1f;
		icicle.transform.position = position;
		icicles.Add(icicle);
	}
}
