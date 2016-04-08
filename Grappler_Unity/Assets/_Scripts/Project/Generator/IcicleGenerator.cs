using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : ItemOnTerrainGenerator {
	private void Awake() {
		base.BaseAwake();
	}

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		int num = 1;
//		float chanceOfSmallIcicleSpawn = 0.05f;

//		float surfaceLength = section.surfaceLength;

		CreateSmallIcicleChunk(section, 0.5f);

//		float dist = 1f / ((float)num + 1f);
//		float distVar = dist / 3f;
//		for (int i = 1; i <= num; i++) {
//			float percent = dist * i + Random.Range(-distVar, distVar);
//			
//			if (Random.value < chanceOfSmallIcicleSpawn) CreateSmallIcicleChunk(section, percent);
//		}
	}
	
	private void CreateSmallIcicleChunk(WhitTerrainSection section, float percent) {
		float chunkSize = 0.1f;
		float placeVar = 0.05f;
		int num = Random.Range(1, 5);
		float dist = chunkSize / (num + 1);
		float initialSpot = percent - chunkSize / 2;
		for (int i = 1; i <= num; i++) {
			float iciclePlace = initialSpot + i * dist + Random.Range(-placeVar, placeVar);
			GeneratableItem item = GenerateItemOnSection(section, iciclePlace);
			item.transform.localScale = new Vector3(Random.Range(0.4f, 1f), Random.Range(0.4f, 1f), 1);
		}
	}
}
