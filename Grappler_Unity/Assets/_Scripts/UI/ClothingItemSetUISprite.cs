using UnityEngine;
using System.Collections;

public class ClothingItemSetUISprite : MonoBehaviour {
	[SerializeField] private tk2dSprite sprite;

	private ClothingItemSet itemSet;

	public void SetClothingItemSet(ClothingItemSet itemSet) {
		this.itemSet = itemSet;
		ClothingItem firstItem = itemSet.GetFirstClothingItem();
		if (firstItem == null || !firstItem.HasValidSpriteName()) return;
		float spriteScale = firstItem.GetSpriteScale();
		string spriteName = firstItem.spriteName;
		sprite.SetSprite(spriteName);
		sprite.scale = new Vector3(spriteScale, spriteScale, 1);
	}
}
