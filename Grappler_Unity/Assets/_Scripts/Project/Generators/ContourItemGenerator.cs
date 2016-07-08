using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WhitTerrain;
using WhitDataTypes;

public class ContourItemGenerator : Generator {
	[SerializeField] protected Contour contour;
	[SerializeField] protected float verticalOffset = 0;

	protected override void BaseAwake() {
		base.BaseAwake();
		contour.SignalSegmentAdded += OnSegmentAdded;
		contour.SignalSegmentRemoved += OnSegmentRemoved;
	}

	protected virtual void OnSegmentAdded(ContourSegment segment) {
		
	}

	protected virtual void OnSegmentRemoved(ContourSegment segment) {

	}

	protected List<GeneratableItem> GenerateItemsOnSection(ContourSegment segment, FloatRange deltaDistRange) {
		List<GeneratableItem> items = new List<GeneratableItem>();
		float length = segment.surfaceLength;
		float dist = 0;
		while (true) {
			dist += deltaDistRange.GetRandom();
			if (dist > length) break;
			items.Add(GenerateItemOnSegment(segment, dist));
		}
		return items;
	}

	protected GeneratableItem GenerateItemOnSegment(ContourSegment segment, float relativeSurfaceDist) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = segment.transform;
		Vector3 position = segment.GetSurfacePointAtRelativeDist(relativeSurfaceDist);
		position.y += verticalOffset;
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}

	protected void RecycleItemsOnSection<T>(ContourSegment segment) where T : GeneratableItem {
		var items = segment.GetComponentsInChildren<T>();
		foreach (T item in items) item.RecycleItem();
	}
}