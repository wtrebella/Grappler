using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public class ItemOnTerrainGenerator : Generator {
	[SerializeField] protected Contour contour;

	protected override void BaseAwake() {
		base.BaseAwake();
		contour.SignalSegmentAdded += OnSegmentAdded;
		contour.SignalSegmentRemoved += OnSegmentRemoved;
	}

	protected virtual void OnSegmentAdded(ContourSegment section) {
		
	}

	protected virtual void OnSegmentRemoved(ContourSegment section) {

	}

	protected List<GeneratableItem> GenerateItemsOnSection(ContourSegment section, FloatRange deltaDistRange) {
		List<GeneratableItem> items = new List<GeneratableItem>();
		float length = section.surfaceLength;
		float dist = 0;
		while (true) {
			dist += deltaDistRange.GetRandom();
			if (dist > length) break;
			items.Add(GenerateItemOnSection(section, dist));
		}
		return items;
	}

	protected GeneratableItem GenerateItemOnSection(ContourSegment section, float relativeSurfaceDist) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = section.transform;
		Vector3 position = section.GetSurfacePointAtRelativeDist(relativeSurfaceDist);
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}

	protected void RecycleItemsOnSection<T>(ContourSegment section) where T : GeneratableItem {
		var items = section.GetComponentsInChildren<T>();
		foreach (T item in items) item.RecycleItem();
	}
}