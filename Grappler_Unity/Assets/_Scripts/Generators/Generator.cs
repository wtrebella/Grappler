using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Generator : MonoBehaviour {
	public int numItemsCreated {get; private set;}

	[SerializeField] private int maxItems = 10;
	[SerializeField] private bool autoRecycle = true;
	[SerializeField] protected GeneratableItem prefab;

	protected List<GeneratableItem> items;

	public void HandleItemRecycled(GeneratableItem item) {
		items.Remove(item);
	}

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
	
		items.Add(item);
		item.HandleSpawned(this);

		if (autoRecycle && items.Count > maxItems) RecycleFirstItem();

		return item;
	}

	protected void RecycleFirstItem() {
		if (items.Count == 0) return;
		items[0].RecycleItem();
	}
}