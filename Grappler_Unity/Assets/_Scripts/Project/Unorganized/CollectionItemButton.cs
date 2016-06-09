using UnityEngine;
using System.Collections;

public class CollectionItemButton : MonoBehaviour {
	[SerializeField] private CollectionItemUISprite sprite;

	private CollectionItem item;

	public void SetCollectionItem(CollectionItem item) {
		this.item = item;
		sprite.SetCollectionItem(item);
	}
}