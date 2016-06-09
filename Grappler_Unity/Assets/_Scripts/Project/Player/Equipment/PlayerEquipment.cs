using UnityEngine;
using System.Collections;

public class PlayerEquipment : MonoBehaviour {
	private IEnumerator Start() {
		yield return StartCoroutine(ClothingManager.instance.WaitForInit());
		ClothingManager.instance.WearSavedOrFirstItems();
//		ClothingManager.instance.WearHat(CollectionManager.instance.GetItem(CollectionType.Hats, "Baseball Cap"));
//		ClothingManager.instance.WearShoes(CollectionManager.instance.GetItem(CollectionType.Shoes, "Converse"));
	}
}
