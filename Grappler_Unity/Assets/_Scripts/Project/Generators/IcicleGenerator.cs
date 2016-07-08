using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public class IcicleGenerator : ContourItemGenerator {
	[SerializeField] private FloatRange deltaDistRange = new FloatRange(1.0f, 20.0f);

	protected override void OnSegmentAdded(ContourSegment section) {
		GenerateIcicles(section);
	}

	protected override void OnSegmentRemoved(ContourSegment section) {
		RecycleItemsOnSection<SmallIcicle>(section);
	}

	private void GenerateIcicles(ContourSegment section) {
		GenerateItemsOnSection(section, deltaDistRange);
	}
}