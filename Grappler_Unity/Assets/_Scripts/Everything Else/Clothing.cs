using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
using System.Linq;

public class Clothing : MonoBehaviour {
	[SerializeField] private tk2dSpriteCollectionData spriteCollection;

	[SerializeField] private SkeletonAnimation stickmanTop;
	[SerializeField] private SkeletonAnimation stickmanBottom;

	private ClothingItem[] hats;
	private Dictionary<ClothingItemType, ClothingItem> equippedClothingItems;
	private string hatsSlot = "hat";
	private string hatsPath = "hats/";

	private void Awake() {
		equippedClothingItems = new Dictionary<ClothingItemType, ClothingItem>();
		LoadClothingItems();
	}

	private void LoadClothingItems() {
		hats = Resources.LoadAll("Clothing Items/Hats", typeof(ClothingItem)).Cast<ClothingItem>().ToArray();
	}

	public ClothingItem[] GetHats() {
		return hats;
	}

	public void Equip(ClothingItem clothingItem) {
		SetAttachment(clothingItem);
	}

	public void Unequip(ClothingItemType type) {
		RemoveAttachment(type);
	}

	public bool ItemTypeIsEquipped(ClothingItemType type) {
		return equippedClothingItems.ContainsKey(type);
	}

	public ClothingItem GetEquippedItem(ClothingItemType type) {
		if (!equippedClothingItems.ContainsKey(type)) return null;

		return equippedClothingItems[type];
	}

	private void RemoveAttachment(ClothingItemType type) {
		string slot = GetSlot(type);
		SkeletonAnimation skeleton = GetSkeleton(type);
		if (equippedClothingItems.ContainsKey(type)) equippedClothingItems.Remove(type);
		skeleton.skeleton.SetAttachment(slot, null);
	}

	private void SetAttachment(ClothingItem clothingItem) {
		string slot = GetSlot(clothingItem.type);
		string path = GetPath(clothingItem);
		SkeletonAnimation skeleton = GetSkeleton(clothingItem.skeleton);

		equippedClothingItems.Add(clothingItem.type, clothingItem);
		skeleton.skeleton.SetAttachment(slot, path);
	}

	private SkeletonAnimation GetSkeleton(ClothingItemType type) {
		if (type == ClothingItemType.Hat) return GetSkeleton(ClothingSkeletonType.Top);
		else return null;
	}

	private SkeletonAnimation GetSkeleton(ClothingSkeletonType type) {
		SkeletonAnimation skeleton = null;
		if (type == ClothingSkeletonType.Top) skeleton = stickmanTop;
		else if (type == ClothingSkeletonType.Bottom) skeleton = stickmanBottom;
		else {
			Debug.LogError("invalid skeleton type: " + type.ToString());
			return null;
		}
		return skeleton;
	}

	private string GetPath(ClothingItem clothingItem) {
		if (clothingItem.type == ClothingItemType.Hat) return hatsPath + clothingItem.GetSprite().name;
		else return "";
	}

	private string GetSlot(ClothingItemType type) {
		if (type == ClothingItemType.Hat) return hatsSlot;
		else return "";
	}
	
	private void Update() {
		
	}
}
