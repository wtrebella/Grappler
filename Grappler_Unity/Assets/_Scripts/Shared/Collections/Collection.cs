using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Collection : MonoBehaviour {
	public List<CollectionItemData> items {get; private set;}

	public CollectionType collectionType = CollectionType.None;

	[SerializeField] private string path = "Collections/";

	private void Awake() {
		LoadCollectionItemData();
	}

	private void LoadCollectionItemData() {
		items = Resources.LoadAll(path, typeof(CollectionItemData)).Cast<CollectionItemData>().ToArray().ToList();
	}

	public List<CollectionItemData> GetOwnedItems() {
		var ownedItems = new List<CollectionItemData>();
		foreach (CollectionItemData item in items) {
			if (item.owned) ownedItems.Add(item);
		}
		return ownedItems;
	}

	public List<CollectionItemData> GetUnownedItems() {
		var unownedItems = new List<CollectionItemData>();
		foreach (CollectionItemData item in items) {
			if (!item.owned) unownedItems.Add(item);
		}
		return unownedItems;
	}
}
