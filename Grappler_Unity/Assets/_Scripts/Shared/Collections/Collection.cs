using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Collection : MonoBehaviour {
	public List<CollectionItem> items {get; private set;}

	public CollectionType collectionType = CollectionType.None;

	[SerializeField] private string path = "Collections/";

	private void Awake() {
		LoadCollectionItemData();
	}

	private void LoadCollectionItemData() {
		items = Resources.LoadAll(path, typeof(CollectionItem)).Cast<CollectionItem>().ToArray().ToList();
	}

	public CollectionItem GetItem(string itemName) {
		foreach (CollectionItem item in items) {
			if (item.name == itemName) return item;
		}

		Debug.Log("no item with the name: " + itemName + " in collection: " + collectionType.ToString());
		return null;
	}

	public List<CollectionItem> GetOwnedItems() {
		var ownedItems = new List<CollectionItem>();
		foreach (CollectionItem item in items) {
			if (item.owned) ownedItems.Add(item);
		}
		return ownedItems;
	}

	public List<CollectionItem> GetUnownedItems() {
		var unownedItems = new List<CollectionItem>();
		foreach (CollectionItem item in items) {
			if (!item.owned) unownedItems.Add(item);
		}
		return unownedItems;
	}
}
