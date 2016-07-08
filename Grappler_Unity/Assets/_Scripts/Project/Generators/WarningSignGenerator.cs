using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;

public class WarningSignGenerator : ContourItemGenerator {
	[SerializeField] private Path terrainPair;

	protected override void BaseAwake() {
		base.BaseAwake();
		terrainPair.SignalPatternAdded += OnPatternAdded;
	}

	private void OnPatternAdded(PathPatternType patternType, List<ContourSegment> topSections, List<ContourSegment> bottomSections) {
		if (patternType == PathPatternType.End) {
			GenerateItemOnSegment(bottomSections.GetFirst(), 0);
		}
	}
}
