using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectionItemList : MonoBehaviour {
	[SerializeField] private CollectionItemButton collectionItemButtonPrefab;
	[SerializeField] private CollectionType collectionType = CollectionType.None;

	private Collection collection;
	private List<CollectionItem> items;

	private void Awake() {
		if (!CollectionManager.DoesExist()) return;

		collection = CollectionManager.instance.GetCollection(collectionType);
		items = collection.GetAllItems();

		foreach (CollectionItem item in items) AddCollectionItemButton(item);
	}

	private void AddCollectionItemButton(CollectionItem item) {
		CollectionItemButton button = Instantiate(collectionItemButtonPrefab);
		button.transform.SetParent(transform);
		button.transform.localPosition = Vector3.zero;
		button.transform.localScale = new Vector3(1, 1, 1);
		button.SetCollectionItem(item);
	}
}
