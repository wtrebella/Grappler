using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemOnTerrainGenerator : Generator {
	[SerializeField] protected WhitTerrain terrain;

	protected override void BaseAwake() {
		base.BaseAwake();
		terrain.SignalTerrainSectionAdded += OnTerrainSectionAdded;
		terrain.SignalTerrainSectionRemoved += OnTerrainSectionRemoved;
	}

	protected virtual void OnTerrainSectionAdded(WhitTerrainSection section) {
		
	}

	protected virtual void OnTerrainSectionRemoved(WhitTerrainSection section) {

	}

	protected List<GeneratableItem> GenerateItemsOnSection(WhitTerrainSection section, FloatRange deltaDistRange) {
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

	protected GeneratableItem GenerateItemOnSection(WhitTerrainSection section, float relativeSurfaceDist) {
		GeneratableItem item = GenerateItem();
		item.transform.parent = section.transform;
		Vector3 position = section.GetSurfacePointAtRelativeDist(relativeSurfaceDist);
		position.z += 0.1f;
		item.transform.position = position;
		return item;
	}

	protected void RecycleItemsOnSection<T>(WhitTerrainSection section) where T : GeneratableItem {
		var items = section.GetComponentsInChildren<T>();
		foreach (T item in items) item.RecycleItem();
	}
}