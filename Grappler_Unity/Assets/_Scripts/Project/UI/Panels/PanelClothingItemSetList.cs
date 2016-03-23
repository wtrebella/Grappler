using UnityEngine;
using System.Collections;

public class PanelClothingItemSetList : MonoBehaviour {
	[SerializeField] private ClothingPackageType type = ClothingPackageType.None;
	[SerializeField] private ClothingItemSetButton buttonPrefab;
	[SerializeField] private RectTransform container;

	private void Awake() {
		var list = ClothingDataManager.GetClothingItemSets(type);
		foreach (ClothingPackage itemSet in list) {
			ClothingItemSetButton button = Instantiate(buttonPrefab);
			button.transform.SetParent(container, false);
			button.SetClothingItemSet(itemSet);
		}
	}
}
