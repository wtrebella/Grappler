using UnityEngine;
using System.Collections;

public class EquipmentSlot : MonoBehaviour {
	private string equippedKey {get {return "collectionItem_" + name + "_equipped";}}

	public CollectionType collectionType = CollectionType.None;

	private CollectionItem equippedItem;

	private void Awake() {
		Load();
	}

	public bool IsEquipped() {
		return this.equippedItem != null;
	}

	public CollectionItem GetItem() {
		return this.equippedItem;
	}
	
	public void EquipItem(CollectionItem item) {
		this.equippedItem = item;
		Save();
	}

	public void RemoveItem() {
		this.equippedItem = null;
		Save();
	}

	private void Load() {
		string itemName = WhitPrefs.GetString(equippedKey);
		if (string.IsNullOrEmpty(itemName)) return;
		else EquipItem(CollectionManager.instance.GetItem(collectionType, itemName));
	}

	private void Save() {
		string itemName = IsEquipped() ? this.equippedItem.name : "";
		WhitPrefs.SetString(equippedKey, itemName);
	}
}