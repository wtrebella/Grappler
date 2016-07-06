using UnityEngine;
using System.Collections;
using WhitTerrain;
using WhitDataTypes;

public class TreeGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float sectionProbability = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnSegmentAdded(ContourSegment section) {
		if (Random.value < sectionProbability) GenerateTrees(section);
	}

	protected override void OnSegmentRemoved(ContourSegment section) {
		RecycleItemsOnSection<FirTree>(section);
	}

	private void GenerateTrees(ContourSegment section) {
		GenerateItemsOnSection(section, deltaDistRange);
	}
}
