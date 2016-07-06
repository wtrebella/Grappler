using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using WhitTerrain;

public class AnchorableGenerator : ItemOnTerrainGenerator {
	private static int currentAnchorableID = 0;

	protected override void OnSegmentAdded(ContourSegment section) {
		GenerateAnchorables(section);
	}

	protected override void OnSegmentRemoved(ContourSegment section) {
		RecycleItemsOnSection<Anchorable>(section);
	}

	private void GenerateAnchorables(ContourSegment section) {
		foreach (Vector2 point in section.allPoints) CreateAnchorableAtPoint(section, point);
	}

	private void CreateAnchorableAtPoint(ContourSegment section, Vector2 point) {
		Anchorable anchorable = (Anchorable)GenerateItem();
		anchorable.transform.parent = section.transform;
		anchorable.transform.localPosition = point;
		anchorable.SetAnchorableID(currentAnchorableID++);
	}
}
