using UnityEngine;
using System.Collections;

public class CoinGenerator : ItemBetweenMountainAndGroundGenerator {
	protected override void HandleGroundChunkGenerated(GroundChunk chunk) {
		if (Random.value < 0.5f) {
			float place = Random.Range(0.05f, 0.95f);
			float verticalPlace = Random.Range(0.1f, 0.9f);
			GenerateItemBetweenMountainAndGround(chunk.mountainChunk, chunk, place, verticalPlace);
		}
	}
}
