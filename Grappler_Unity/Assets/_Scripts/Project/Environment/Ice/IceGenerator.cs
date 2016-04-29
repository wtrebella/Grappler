using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IceGenerator : Generator {
	[SerializeField] private WhitTerrainPair terrainPair;

	private void Awake() {
		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(WhitTerrainPairPatternType patternType, List<WhitTerrainSection> topSections, List<WhitTerrainSection> bottomSections) {
		if (patternType != WhitTerrainPairPatternType.Flat) return;


	}
}
