using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClothingItemSetButton : MonoBehaviour {
	[SerializeField] private Button button;
	[SerializeField] private ClothingItemSetUISprite sprite;

	private ClothingManager clothingManager;
	private ClothingPackage itemSet;

	private void Awake() {
		clothingManager = GameObject.FindObjectOfType<ClothingManager>();
	}

	public void SetClothingItemSet(ClothingPackage itemSet) {
		this.itemSet = itemSet;
		sprite.SetClothingItemSet(itemSet);
		if (clothingManager != null) button.onClick.AddListener(() => HandleClick());
	}

	private void HandleClick() {
		if (itemSet == null) return;
		if (!itemSet.isLocked) clothingManager.EquipItemSet(itemSet);
	}
}
