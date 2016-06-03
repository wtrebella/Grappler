using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WarningSignGenerator : ItemOnTerrainGenerator {
	[SerializeField] private WhitTerrainPair terrainPair;

	protected override void BaseAwake() {
		base.BaseAwake();
		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(WhitTerrainPairPatternType patternType, List<WhitTerrainSection> topSections, List<WhitTerrainSection> bottomSections) {
		if (patternType == WhitTerrainPairPatternType.End) {
			GenerateItemOnSection(bottomSections.GetFirst(), 0);
		}
	}
}
