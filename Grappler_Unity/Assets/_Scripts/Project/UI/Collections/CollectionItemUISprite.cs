using UnityEngine;
using System.Collections;

public class CollectionItemUISprite : MonoBehaviour {
	[SerializeField] private tk2dSprite sprite;

	public void SetCollectionItem(CollectionItem item) {
		CollectionItemSprite firstSprite = item.GetFirstSprite();
		tk2dSpriteCollectionData spriteCollection = firstSprite.GetSpriteCollectionData();
		if (firstSprite == null || !firstSprite.HasValidSpriteName()) return;
		float spriteScale = firstSprite.GetSpriteScale();
		string spriteName = firstSprite.spriteName;
		sprite.SetSprite(spriteCollection, spriteName);
		sprite.scale = new Vector3(spriteScale, spriteScale, 1);
	}
}