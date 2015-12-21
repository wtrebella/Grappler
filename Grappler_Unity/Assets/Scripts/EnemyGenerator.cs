using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;
	
	private void Awake() {
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkGenerated;
	}

	private void HandleMountainChunkGenerated(MountainChunk mountainChunk) {
		Enemy enemy = Instantiate(enemyPrefab) as Enemy;
		enemy.SetMountainChunk(mountainChunk);
	}
}
