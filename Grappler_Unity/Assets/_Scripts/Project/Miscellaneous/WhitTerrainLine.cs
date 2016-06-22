using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class WhitTerrainLine : MonoBehaviour {
	[SerializeField] private WhitTerrain terrain;
	[SerializeField] private SnowPuff snowPuffPrefab;

	private Dictionary<WhitTerrainSection, List<SnowPuff>> snowPuffDict;

	private void Awake() {
		snowPuffDict = new Dictionary<WhitTerrainSection, List<SnowPuff>>();

		terrain.SignalTerrainSectionAdded += OnSectionAdded;
		terrain.SignalTerrainSectionRemoved += OnSectionRemoved;
	}

	private void Start() {
		WhitTerrainSection firstSection = terrain.GetFirstSection();
		if (!snowPuffDict.ContainsKey(firstSection)) {
			AddSnowPuffsToSection(firstSection);
		}
	}

	private void AddSnowPuffsToSection(WhitTerrainSection section) {
		var points = section.allPoints;
		snowPuffDict.Add(section, new List<SnowPuff>());
		for (int i = 0; i < points.Count - 1; i++) {
			SnowPuff snowPuff = snowPuffPrefab.Spawn();
			snowPuff.transform.SetParent(transform);
			Vector2 pointA = points[i];
			Vector2 pointB = points[i+1];
			snowPuff.SetPoints(pointA, pointB);
			snowPuffDict[section].Add(snowPuff);
		}
	}

	private void RemoveSnowPuffsFromSection(WhitTerrainSection section) {
		var snowPuffs = snowPuffDict[section];
		for (int i = snowPuffs.Count - 1; i >= 0; i--) {
			SnowPuff snowPuff = snowPuffs[i];
			snowPuffs.RemoveAt(i);
			snowPuff.Recycle();
		}
		snowPuffDict.Remove(section);
	}

	private void OnSectionAdded(WhitTerrainSection section) {
		AddSnowPuffsToSection(section);
	}

	private void OnSectionRemoved(WhitTerrainSection section) {
		RemoveSnowPuffsFromSection(section);
	}
	
	private void Update() {
	
	}
}
