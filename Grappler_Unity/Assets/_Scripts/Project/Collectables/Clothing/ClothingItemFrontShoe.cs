using UnityEngine;
using System.Collections;

public class ClothingItemFrontShoe : ClothingItem {
	private void OnEnable() {
		type = ClothingItemType.ShoeFront;
	}
}
