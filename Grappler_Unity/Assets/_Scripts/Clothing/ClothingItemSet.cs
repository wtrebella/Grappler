using UnityEngine;
using System.Collections;

public enum ClothingItemSetType {
	None = 0,
	Hat,
	Shoes,
	MAX
}

[System.Serializable]
public class ClothingItemSet : ScriptableObject {
	public ClothingItem[] items;
	public ClothingItemSetType type = ClothingItemSetType.None;
}
