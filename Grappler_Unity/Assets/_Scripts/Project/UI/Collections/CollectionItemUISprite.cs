using UnityEngine;
using System.Collections;

public class CollectionItemUISprite : MonoBehaviour {
	[SerializeField] private tk2dSprite sprite;

	private CollectionItem itemSet;

	public void SetCollectionItem(CollectionItem item) {
		this.itemSet = item;
		CollectionItemSprite firstSprite = item.GetFirstSprite();
		if (firstSprite == null || !firstSprite.HasValidSpriteName()) return;
		float spriteScale = firstSprite.GetSpriteScale();
		string spriteName = firstSprite.spriteName;
		sprite.SetSprite(spriteName);
		sprite.scale = new Vector3(spriteScale, spriteScale, 1);
	}
}