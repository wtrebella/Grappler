using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Generator : MonoBehaviour {
	public int numItemsCreated {get; private set;}

	[SerializeField] protected GeneratableItem prefab;

	protected List<GeneratableItem> items;

	private void Awake() {
		BaseAwake();
	}

	protected virtual void BaseAwake() {
		numItemsCreated = 0;
		items = new List<GeneratableItem>();
	}

	protected void GenerateItems(int numToGenerate) {
		for (int i = 0; i < numToGenerate; i++) GenerateItem();
	}

	protected GeneratableItem GenerateItem() {
		numItemsCreated++;

		GeneratableItem item = prefab.Spawn();
		item.transform.parent = transform;
	
		HandleItemGenerated(item);
		items.Add(item);
		item.GenerationComplete(this);

		return item;
	}

	protected void RecycleFirstItem() {
		if (items.Count == 0) return;

		GeneratableItem firstItem = items[0];
		items.Remove(firstItem);
		firstItem.Recycle();
	}

	protected virtual void HandleItemGenerated(GeneratableItem item) {

	}
}