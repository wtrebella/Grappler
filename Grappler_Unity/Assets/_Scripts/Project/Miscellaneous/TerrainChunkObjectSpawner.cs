using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Polydraw;

public class TerrainChunkObjectSpawner : MonoBehaviour {
	public GameObject prefab;

	private PolydrawObject poly;

	private void Awake() {
		poly = GetComponentInParent<PolydrawObject>();
		if (poly == null) Debug.LogError("no PolydrawObject parent!");
	}
	
	private void Start() {
		if (poly == null) return;

		SpawnObjectsAlongBorder();
	}

	private void SpawnObject(Vector2 point) {
		GameObject spawnedObject = Instantiate(prefab);
		spawnedObject.transform.SetParent(transform);
		spawnedObject.transform.position = point;
	}

	private void SpawnObjectsAlongBorder() {
		List<Vector2> borderPoints = poly.GetWorldBorderPoints();
		foreach (Vector2 point in borderPoints) SpawnObject(point);
	}
	
	private void Update() {
	
	}
}
