using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
using System.Linq;
using UnityEngine.Events;

public class ClothingManager : MonoBehaviour {
	public UnityEvent OnHatEquipped;
	public UnityEvent OnShoeBackEquipped;
	public UnityEvent OnShoeFrontEquipped;

	public UnityEvent OnHatUnequipped;
	public UnityEvent OnShoeBackUnequipped;
	public UnityEvent OnShoeFrontUnequipped;

	[SerializeField] private tk2dSpriteCollectionData spriteCollection;
	[SerializeField] private SkeletonAnimation stickmanTop;
	[SerializeField] private SkeletonAnimation stickmanBottom;

	private string spritePathHats = "hats/";
	private string spritePathShoesBack = "shoes/shoesBack/";
	private string spritePathShoesFront = "shoes/shoesFront/";

	private string hatSlot = "hat";
	private string shoeBackSlot = "shoeBack";
	private string shoeFrontSlot = "shoeFront";

	private void Start() {
		EquipSavedItemSets();
		EquipFirstHatIfNoneEquipped();
		EquipFirstShoesIfNoneEquipped();
	}

	public void EquipFirstHatIfNoneEquipped() {
		if (!ItemSetIsEquipped(ClothingPackageType.Hat)) EquipFirstHat();
	}

	public void EquipFirstShoesIfNoneEquipped() {
		if (!ItemSetIsEquipped(ClothingPackageType.Shoes)) EquipFirstShoes();
	}

	public void EquipFirstHat() {
		EquipItemSet(ClothingDataManager.hats[0]);
	}

	public void EquipFirstShoes() {
		EquipItemSet(ClothingDataManager.shoes[0]);
	}

	public void EquipNextHat() {
		EquipNext(ClothingPackageType.Hat);
	}

	public void EquipNextShoes() {
		EquipNext(ClothingPackageType.Shoes);
	}

	public void EquipNext(ClothingPackageType type) {
		ClothingPackage equippedItemSet = GetEquippedItemSet(type);

		int currentItemIndex = 0;
		List<ClothingPackage> sets = ClothingDataManager.GetClothingItemSets(type);

		if (equippedItemSet != null) {
			for (int i = 0; i < sets.Count; i++) {
				ClothingPackage arrayItemSet = sets[i];
				if (equippedItemSet == arrayItemSet) {
					currentItemIndex = i;
				}
			}
		}

		IntRange wrapRange = new IntRange(0, sets.Count);
		int newItemIndex = WhitTools.IncrementWithWrap(currentItemIndex, wrapRange);
		ClothingPackage nextItem = sets[newItemIndex];
		EquipItemSet(nextItem);
	}

	public void EquipItemSet(ClothingPackage clothingItemSet) {
		UnequipItemSet(clothingItemSet.type);
		SetClothingItemSet(clothingItemSet);
	}

	public void UnequipItemSet(ClothingPackageType type) {
		if (ItemSetIsEquipped(type)) RemoveClothingItemSet(type);
	}

	public bool ItemSetIsEquipped(ClothingPackageType type) {
		return ClothingDataManager.ItemSetTypeIsEquipped(type);
	}

	public ClothingPackage GetEquippedItemSet(ClothingPackageType type) {
		if (!ClothingDataManager.ItemSetTypeIsEquipped(type)) return null;

		return ClothingDataManager.GetEquippedItemSet(type);
	}

	private void EquipSavedItemSets() {
		var equippedSets = ClothingDataManager.equippedSets.ToArray();
		foreach (ClothingPackage itemSet in equippedSets) EquipItemSet(itemSet);
	}

	private void SetClothingItemSet(ClothingPackage itemSet) {
		foreach (CollectableItem item in itemSet.items) {
			ClothingItem clothingItem = (ClothingItem)item;
			if (!clothingItem.HasValidSpriteName()) Debug.LogError("item doesn't have a valid sprite! fix this or expect errors.");
			SetAttachment(clothingItem);
		}
		if (!ClothingDataManager.ItemSetTypeIsEquipped(itemSet.type)) ClothingDataManager.equippedSets.Add(itemSet);
	}

	private void RemoveClothingItemSet(ClothingPackageType type) {
		ClothingPackage itemSet = GetEquippedItemSet(type);
		foreach (CollectableItem item in itemSet.items) {
			ClothingItem clothingItem = (ClothingItem)item;
			RemoveAttachment(clothingItem.type);
		}
		ClothingDataManager.RemoveEquippedItemSet(type);
	}

	private void SetAttachment(ClothingItem clothingItem) {
		string slot = GetSlot(clothingItem.type);
		string path = GetSpritePath(clothingItem);
		SkeletonAnimation skeleton = GetSkeleton(clothingItem.skeleton);

		skeleton.skeleton.SetAttachment(slot, path);

		if (clothingItem.type == ClothingItemType.Hat) WhitTools.Invoke(OnHatEquipped);
		else if (clothingItem.type == ClothingItemType.ShoeBack) WhitTools.Invoke(OnShoeBackEquipped);
		else if (clothingItem.type == ClothingItemType.ShoeFront) WhitTools.Invoke(OnShoeFrontEquipped);
	}

	private void RemoveAttachment(ClothingItemType type) {
		string slot = GetSlot(type);
		SkeletonAnimation skeleton = GetSkeleton(type);

		skeleton.skeleton.SetAttachment(slot, null);

		if (type == ClothingItemType.Hat) WhitTools.Invoke(OnHatUnequipped);
		else if (type == ClothingItemType.ShoeBack) WhitTools.Invoke(OnShoeBackUnequipped);
		else if (type == ClothingItemType.ShoeFront) WhitTools.Invoke(OnShoeFrontUnequipped);
	}

	private SkeletonAnimation GetSkeleton(ClothingItemType type) {
		SkeletonAnimation skeleton = null;
		if (type == ClothingItemType.Hat) skeleton = GetSkeleton(ClothingSkeletonType.Top);
		else if (type == ClothingItemType.ShoeBack) skeleton = GetSkeleton(ClothingSkeletonType.Bottom); 
		else if (type == ClothingItemType.ShoeFront) skeleton = GetSkeleton(ClothingSkeletonType.Bottom); 
		else {
			Debug.LogError("invalid clothing item type: " + type.ToString());
			return null;
		}
		return skeleton;
	}

	private SkeletonAnimation GetSkeleton(ClothingSkeletonType type) {
		SkeletonAnimation skeleton = null;
		if (type == ClothingSkeletonType.Top) skeleton = stickmanTop;
		else if (type == ClothingSkeletonType.Bottom) skeleton = stickmanBottom;
		else Debug.LogError("invalid skeleton type: " + type.ToString());
		return skeleton;
	}

	private string GetSpritePath(ClothingItem clothingItem) {
		string path;

		if (clothingItem.type == ClothingItemType.Hat) path = spritePathHats + clothingItem.spriteName;
		else if (clothingItem.type == ClothingItemType.ShoeBack) path = spritePathShoesBack + clothingItem.spriteName;
		else if (clothingItem.type == ClothingItemType.ShoeFront) path = spritePathShoesFront + clothingItem.spriteName;
		else {
			Debug.LogError("invalid clothing item type: " + clothingItem.type.ToString());
			path = "";
		}

		return path;
	}

	private string GetSlot(ClothingItemType type) {
		if (type == ClothingItemType.Hat) return hatSlot;
		else if (type == ClothingItemType.ShoeBack) return shoeBackSlot;
		else if (type == ClothingItemType.ShoeFront) return shoeFrontSlot;
		else {
			Debug.LogError("invalid clothing item type: " + type.ToString());
			return "";
		}
	}

	private void OnEnable() {
		ClothingDataManager.OnEnable();
	}

	private void OnDisable() {
		ClothingDataManager.OnDisable();
	}
}
