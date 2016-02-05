using UnityEngine;
using System.Collections;

public class CoinGenerator : MonoBehaviour {
	[SerializeField] private Coin coinPrefab;

	private void Awake() {

	}
	
	private void HandleMountainChunkGenerated(MountainChunk mountainChunk) {
		for (int i = 0; i < 10; i++) CreateCoin(mountainChunk);
	}
	
	private void CreateCoin(MountainChunk mountainChunk) {
		Coin coin = coinPrefab.Spawn();
		coin.transform.parent = transform;
		coin.transform.localPosition = Vector3.zero;
		Vector3 deltaPos = new Vector3(1, -1);
		Vector2 mountainPos = mountainChunk.PlaceToPosition(Random.value);
		Vector3 position = new Vector3(mountainPos.x + deltaPos.x, mountainPos.y + deltaPos.y, 0);
		coin.transform.position = position;
	}
}
