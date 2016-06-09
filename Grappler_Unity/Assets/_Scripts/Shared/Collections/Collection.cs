using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Collection : MonoBehaviour {
	private List<CollectionItem> _items;
	public List<CollectionItem> items {
		get {
			if (_items == null) LoadItems();
			return _items;
		}
	}

	public CollectionType collectionType = CollectionType.None;

	[SerializeField] private string path = "Collections/";

	public CollectionItem GetFirstItem() {
		return GetItem(0);
	}

	public CollectionItem GetLastItem() {
		return GetItem(GetItemCount() - 1);
	}

	public CollectionItem GetItem(string itemName) {
		foreach (CollectionItem item in items) {
			if (item.name == itemName) return item;
		}

		Debug.Log("no item with the name: " + itemName + " in collection: " + collectionType.ToString());
		return null;
	}

	public CollectionItem GetItem(int index) {
		if (!HasItems()) {
			Debug.LogError("no items to get!");
			return null;
		}
		if (GetItemCount() <= index) {
			Debug.LogError("trying to get item at index " + index + " but there are only " + GetItemCount() + " items available!");
			return null;
		}
		return items[index];
	}

	public int GetItemCount() {
		if (!HasItems()) return 0;
		else return items.Count;
	}

	public bool HasItems() {
		return items != null && items.Count > 0;
	}
		
	public List<CollectionItem> GetAllItems() {
		return items;
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
		
	private void LoadItems() {
		_items = Resources.LoadAll(path, typeof(CollectionItem)).Cast<CollectionItem>().ToArray().ToList();
	}
}
