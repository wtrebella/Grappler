using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionItemButton : MonoBehaviour {
	[SerializeField] private CollectionItemUISprite sprite;
	[SerializeField] private Button button;

	private CollectionItem item;

	public void SetCollectionItem(CollectionItem item) {
		this.item = item;
		SetupSprite(item);
		SetupButton(item);
	}

	private void SetupButton(CollectionItem item) {
		button.onClick.AddListener(delegate {OnButtonClicked(item);});
	}

	private void SetupSprite(CollectionItem item) {
		sprite.SetCollectionItem(item);
	}

	public void OnButtonClicked(CollectionItem item) {
		ClothingManager.instance.WearItem(item);
	}
}