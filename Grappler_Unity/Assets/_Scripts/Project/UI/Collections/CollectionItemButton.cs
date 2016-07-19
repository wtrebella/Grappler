using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionItemButton : MonoBehaviour {
	[SerializeField] private CollectionItemUISprite sprite;
	[SerializeField] private Button button;

	public void SetCollectionItem(CollectionItem item) {
		SetupSprite(item);
		SetupButton(item);
	}

	private void SetupButton(CollectionItem item) {
		if (item.owned)	button.onClick.AddListener(delegate {OnButtonClicked(item);});
		else button.interactable = false;
	}

	private void SetupSprite(CollectionItem item) {
		sprite.SetCollectionItem(item);
	}

	public void OnButtonClicked(CollectionItem item) {
		ClothingManager.instance.WearItem(item);
	}
}