using UnityEngine;
using System.Collections;

public class ClothingItemSetButton : MonoBehaviour {
	[SerializeField] private ClothingItemSetUISprite sprite;

	private ClothingItemSet itemSet;

	public void SetClothingItemSet(ClothingItemSet itemSet) {
		this.itemSet = itemSet;
		sprite.SetClothingItemSet(itemSet);
	}
}
