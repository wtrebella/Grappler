using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RockGenerator : MonoBehaviour {
	[SerializeField] private Transform rockPrefab;
	[SerializeField] private int numRocksPerMountainChunk = 100;
	[SerializeField] private MountainChunkGenerator mountainChunkGenerator;

	private void Awake() {
		mountainChunkGenerator.SignalMountainChunkCreated += HandleMountainChunkCreated;
	}

	private void HandleMountainChunkCreated(MountainChunk mountainChunk) {
		float distPerRock = 1f/(numRocksPerMountainChunk);
		float dist = distPerRock;
		for (int i = 0; i < numRocksPerMountainChunk; i++) {
			Vector2 position = mountainChunk.GetPositionAlongLine(dist);
			GenerateRock(mountainChunk, position);
			dist += distPerRock;
		}
	}

	private void GenerateRock(MountainChunk mountainChunk, Vector2 position) {
		Transform rock = rockPrefab.Spawn();
		rock.parent = mountainChunk.transform;
		rock.localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 359));
		rock.position = position;
	}
}
