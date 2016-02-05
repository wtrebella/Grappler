using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Generator : MonoBehaviour {
	public Action<GeneratableItem> SignalItemGenerated;
	public int numItemsCreated {get; private set;}

	[SerializeField] protected GeneratableItem prefab;

	protected List<GeneratableItem> items;

	private void Awake() {
		BaseAwake();
	}

	protected void BaseAwake() {
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
		item.GenerationComplete(this);

		if (SignalItemGenerated != null) SignalItemGenerated(item);

		HandleGeneratedItem(item);

		return item;
	}

	protected virtual void HandleGeneratedItem(GeneratableItem item) {

	}
}