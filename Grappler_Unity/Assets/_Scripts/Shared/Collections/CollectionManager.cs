using UnityEngine;
using System.Collections;

public class CollectionManager : MonoBehaviour {
	public static CollectionManager instance;

	public Collection[] collectionPrefabs;
	public Collection[] collections;

	public CollectionItem GetItem(CollectionType collectionType, string itemName) {
		Collection collection = GetCollection(collectionType);
		CollectionItem item = collection.GetItem(itemName);
		return item;
	}

	private void Awake() {
		InitializeSingleton();
		LoadCollections();
	}

	private void InitializeSingleton() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this.gameObject);
		}
	}

	private void LoadCollections() {
		collections = new Collection[collectionPrefabs.Length];
		for (int i = 0 ; i < collectionPrefabs.Length; i++) {
			Collection collectionPrefab = collectionPrefabs[i];
			Collection collection = Instantiate(collectionPrefab).transform.SetParent(this.transform);
			collections[i] = collection;
		}
	}

	private Collection GetCollection(CollectionType collectionType) {
		foreach (Collection collection in collections) {
			if (collectionType == collection.collectionType) return collection;
		}

		Debug.LogError("no collection with type: " + collectionType.ToString());

		return null;
	}
}
