using UnityEngine;
using System.Collections;

public class MountainEnemyGenerator : MonoBehaviour {
	[SerializeField] private MountainEnemy enemyPrefab;

	private void Awake() {

	}

	private void HandleMountainChunkGenerated(MountainChunk mountainChunk) {
		float linePosition = Random.Range(0.03f, 0.1f);
		for (int i = 0; i < 9; i++) {
			CreateEnemy(mountainChunk, linePosition);
			linePosition += Random.Range(0.02f, 0.1f);
		}
	}

	private void CreateEnemy(MountainChunk mountainChunk, float linePosition) {
		MountainEnemy enemy = enemyPrefab.Spawn();
		enemy.transform.parent = transform;
		enemy.transform.localPosition = Vector3.zero;
		enemy.SetMountainChunk(mountainChunk);
		enemy.SetExactLinePosition(linePosition);
	}
}
