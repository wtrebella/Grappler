using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcicleGenerator : ItemOnTerrainGenerator {
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(1.0f, 20.0f);

	protected override void OnTerrainSectionAdded(WhitTerrainSection section) {
		GenerateIcicles(section);
	}

	protected override void OnTerrainSectionRemoved(WhitTerrainSection section) {
		RecycleItemsOnSection<SmallIcicle>(section);
	}

	private void GenerateIcicles(WhitTerrainSection section) {
		GenerateItemsOnSection(section, deltaDistRange);
	}
}