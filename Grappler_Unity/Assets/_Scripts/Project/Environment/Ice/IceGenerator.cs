using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;

public class IceGenerator : Generator {
	[SerializeField] private Path terrainPair;

	private void Awake() {
		BaseAwake();

		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(PathPatternType patternType, List<ContourSegment> topSections, List<ContourSegment> bottomSections) {
		if (patternType != PathPatternType.Flat) return;

		ContourSegment section = bottomSections.GetFirst();
		Ice ice = GenerateItem<Ice>();
		ice.SetSection(section);
	}
}
