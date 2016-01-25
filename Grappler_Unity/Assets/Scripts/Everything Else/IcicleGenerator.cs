using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : MonoBehaviour {
	[SerializeField] private SmallIcicle smallIciclePrefab;
	[SerializeField] private BigIcicle bigIciclePrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private List<SmallIcicle> icicles;
	
	private void Awake() {
		icicles = new List<SmallIcicle>();
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk chunk) {
		int num = 20;
		float chanceOfSpawn = 0.2f;
		float dist = 1f / ((float)num + 1f);
		float distVar = dist / 3f;
		for (int i = 1; i <= num; i++) {
			float place = dist * i + Random.Range(-distVar, distVar);
			
			if (Random.value < chanceOfSpawn) CreateSmallIcicleChunk(chunk, place);
		}
	}

	private void CreateBigIcicle(MountainChunk chunk, float place) {
//		BigIcicle icicle = bigIciclePrefab.Spawn();
//		icicle.transform.localScale = new Vector3(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
//		icicle.transform.parent = chunk.transform;
//		Vector3 position = chunk.GetPositionAtPlace(place);;
//		position.z += 0.1f;
//		icicle.transform.position = position;
//		icicles.Add(icicle);
	}

	private void CreateSmallIcicleChunk(MountainChunk chunk, float place) {
		float chunkSize = 0.04f;
		float placeVar = 0.01f;
		int num = Random.Range(1, 5);
		float dist = chunkSize / (num + 1);
		float initialSpot = place - chunkSize / 2;
		for (int i = 1; i <= num; i++) {
			float iciclePlace = initialSpot + i * dist + Random.Range(-placeVar, placeVar);
			CreateSmallIcicle(chunk, iciclePlace);
		}
	}
	
	private void CreateSmallIcicle(MountainChunk chunk, float place) {
		SmallIcicle icicle = smallIciclePrefab.Spawn();
		icicle.transform.localScale = new Vector3(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
		icicle.transform.parent = chunk.transform;
		Vector3 position = chunk.GetPositionAtPlace(place);;
		position.z += 0.1f;
		icicle.transform.position = position;
		icicles.Add(icicle);
	}
}
