using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GiftSceneManager : MonoBehaviour {
	[SerializeField] private Gift gift;

	private void Start() {
		if (CollectionManager.instance == null) return;

		CollectionType collectionType = (CollectionType)UnityEngine.Random.Range((int)CollectionType.NONE + 1, (int)CollectionType.MAX);
		Collection collection = CollectionManager.instance.GetCollection(collectionType);
		CollectionItem item = collection.GetRandomItem();
		gift.SetCollectionItem(item);
	}
	
	private void Update() {
	
	}
}
