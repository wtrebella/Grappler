﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Polydraw;

public class TerrainSpawnTest : MonoBehaviour {
	public GameObject prefab;

	private PolydrawObject poly;

	private void Awake() {
		poly = GetComponent<PolydrawObject>();
	}
	
	private void Start() {
		if (poly == null) return;
		var points = poly.GetWorldBorderPoints();
		foreach (Vector2 point in points) {
			Instantiate(prefab, point, Quaternion.identity);
		}
	}
	
	private void Update() {
	
	}
}
