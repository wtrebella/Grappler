using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClothingItemSetButton : MonoBehaviour {
	[SerializeField] private Button button;
	[SerializeField] private ClothingItemSetUISprite sprite;

	private ClothingManager clothingManager;
	private ClothingItemSet itemSet;

	private void Awake() {
		clothingManager = GameObject.FindObjectOfType<ClothingManager>();
	}

	public void SetClothingItemSet(ClothingItemSet itemSet) {
		this.itemSet = itemSet;
		sprite.SetClothingItemSet(itemSet);
		if (clothingManager != null) button.onClick.AddListener(() => HandleClick(itemSet));
	}

	private void HandleClick(ClothingItemSet itemSet) {
		if (!itemSet.isLocked) clothingManager.EquipItemSet(itemSet);
	}
}
