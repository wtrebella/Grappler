using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	
	private void Awake() {
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkGenerated;
	}

	private void HandleMountainChunkGenerated(MountainChunk mountainChunk) {
		for (int i = 0; i < 10; i++) CreateEnemy(mountainChunk);
	}

	private void CreateEnemy(MountainChunk mountainChunk) {
		Enemy enemy = Instantiate(enemyPrefab) as Enemy;
		enemy.transform.parent = transform;
		enemy.transform.localPosition = Vector3.zero;
		enemy.SetMountainChunk(mountainChunk);
	}
}
