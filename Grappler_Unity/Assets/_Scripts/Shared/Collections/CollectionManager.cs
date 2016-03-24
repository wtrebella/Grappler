using UnityEngine;
using System.Collections;

public class CollectionManager : AutoSingleton<CollectionManager> {
	private Collection[] collections;

	public CollectionItem GetItem(CollectionType collectionType, string itemName) {
		Collection collection = GetCollection(collectionType);
		CollectionItem item = collection.GetItem(itemName);
		return item;
	}

	private void Awake() {
		LoadCollectionsIfNeeded();
	}

	public Collection GetCollection(CollectionType collectionType) {
		LoadCollectionsIfNeeded();

		foreach (Collection collection in collections) {
			if (collectionType == collection.collectionType) return collection;
		}

		Debug.LogError("no collection with type: " + collectionType.ToString());

		return null;
	}

	private void LoadCollectionsIfNeeded() {
		if (!HasCollections()) LoadCollections();
	}

	private bool HasCollections() {
		return collections != null && collections.Length > 0;
	}

	private void LoadCollections() {
		collections = GetComponentsInChildren<Collection>();
	}
}
