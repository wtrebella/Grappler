using UnityEngine;
using System.Collections;
using WhitTerrain;

public class CragGenerator : ItemOnTerrainGenerator {
	[SerializeField] private float cragProbibility = 0.5f;
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(3.0f, 30.0f);

	protected override void OnTerrainSectionAdded(ContourSection section) {
		if (Random.value < cragProbibility) GenerateCrag(section);
	}

	protected override void OnTerrainSectionRemoved(ContourSection section) {
		RecycleItemsOnSection<Crag>(section);
	}

	private void GenerateCrag(ContourSection section) {
		GenerateItemOnSection(section, deltaDistRange.GetRandom());
	}
}
