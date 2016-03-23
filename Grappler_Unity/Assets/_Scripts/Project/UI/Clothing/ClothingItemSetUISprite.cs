using UnityEngine;
using System.Collections;

public class ClothingItemSetUISprite : MonoBehaviour {
	[SerializeField] private tk2dSprite sprite;

	public void SetClothingItemSet(ClothingPackage itemSet) {
		ClothingItem firstItem = (ClothingItem)itemSet.GetFirstItem();
		if (firstItem == null || !firstItem.HasValidSpriteName()) return;
		float spriteScale = firstItem.GetSpriteScale();
		string spriteName = firstItem.spriteName;
		sprite.SetSprite(spriteName);
		sprite.scale = new Vector3(spriteScale, spriteScale, 1);
	}
}
