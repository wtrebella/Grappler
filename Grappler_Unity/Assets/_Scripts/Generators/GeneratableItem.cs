using UnityEngine;
using System.Collections;
using System;

public class GeneratableItem : MonoBehaviour {
	private Generator parentGenerator;

	public virtual void HandleSpawned(Generator generator) {
		parentGenerator = generator;
	}

	public T To<T>() {
		return (T)(object)this;
	}

	public void RecycleItem() {
		this.Recycle();
		RecycleAllAttachedGeneratableItems();
		HandleRecycled();
	}

	protected void RecycleAllAttachedGeneratableItems() {
		var attachedItems = GetComponentsInChildren<GeneratableItem>();
		foreach (GeneratableItem item in attachedItems) {
			if (item == this) continue;
			item.RecycleItem();
		}
	}

	protected virtual void HandleRecycled() {
		if (parentGenerator == null) Debug.LogError("parent generator hasn't been set!");
		parentGenerator.HandleItemRecycled(this);
	}
}
