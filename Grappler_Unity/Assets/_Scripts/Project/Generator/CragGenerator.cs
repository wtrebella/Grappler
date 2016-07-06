using UnityEngine;
using System.Collections;
using WhitTerrain;
using WhitDataTypes;

public class CragGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float cragProbibility = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnSegmentAdded(ContourSegment section) {
		if (Random.value < cragProbibility) GenerateCrag(section);
	}

	protected override void OnSegmentRemoved(ContourSegment section) {
		RecycleItemsOnSection<Crag>(section);
	}

	private void GenerateCrag(ContourSegment section) {
		GenerateItemOnSection(section, deltaDistRange.GetRandom());
	}
}
