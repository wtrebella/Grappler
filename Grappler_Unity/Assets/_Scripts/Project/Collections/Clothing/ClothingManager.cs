using UnityEngine;
using System.Collections;

public class ClothingManager : MonoBehaviour {
	[SerializeField] private WhitSpineSkeleton topSkeleton;
	[SerializeField] private WhitSpineSkeleton bottomSkeleton;

	[SerializeField] private WhitSpineSlot hatSlot;
	[SerializeField] private WhitSpineSlot backShoeSlot;
	[SerializeField] private WhitSpineSlot frontShoeSlot;

	[SerializeField] private string hatSpritesPath = "hats/";
	[SerializeField] private string backShoeSpritesPath = "shoes/shoesBack/";
	[SerializeField] private string frontShoeSpritesPath = "shoes/shoesFront/";

	//	private void 

	//	public UnityEvent OnHatEquipped;
	//	public UnityEvent OnShoeBackEquipped;
	//	public UnityEvent OnShoeFrontEquipped;
	//
	//	public UnityEvent OnHatUnequipped;
	//	public UnityEvent OnShoeBackUnequipped;
	//	public UnityEvent OnShoeFrontUnequipped;
	//
	//	private void Start() {
	//		EquipSavedItemSets();
	//		EquipFirstHatIfNoneEquipped();
	//		EquipFirstShoesIfNoneEquipped();
	//	}
	//
	//	public void EquipFirstHatIfNoneEquipped() {
	//		if (!CollectablePackageDataManager.EquipSlotIsEquipped(EquipSlotType.Hat)) EquipFirstHat();
	//	}
	//
	//	public void EquipFirstShoesIfNoneEquipped() {
	//		if (!ItemSetIsEquipped(ClothingPackageType.Shoes)) EquipFirstShoes();
	//	}
	//
	//	public void EquipFirstHat() {
	//		EquipPackage(ClothingPackageDataManager.hats[0]);
	//	}
	//
	//	public void EquipFirstShoes() {
	//		EquipPackage(ClothingPackageDataManager.shoes[0]);
	//	}
	//
	//	public void EquipHatPackage(ClothingPackage hatPackage) {
	//		CollectablePackageDataManager.Unequip(EquipSlotType.Hat);
	//		UnequipHatPackage(hatPackage.type);
	//		SetHatPackage(hatPackage);
	//		ClothingPackageDataManager.Equip(EquipSlotType.Hat, hatPackage);
	//	}
	//
	//	public void UnequipHatPackage(ClothingPackageType type) {
	//		if (ItemSetIsEquipped(type)) RemoveHatPackage(type);
	//	}
	//
	//	public ClothingPackage GetEquippedPackage(ClothingPackageType type) {
	//		if (!ClothingPackageDataManager.ItemSetTypeIsEquipped(type)) return null;
	//
	//		return ClothingPackageDataManager.GetEquippedItemSet(type);
	//	}
	//
	//	private void EquipSavedItemSets() {
	//		foreach (ClothingPackage package in CollectablePackageDataManager.equippedPackages) EquipPackage(package);
	//	}
	//
	//	private void SetHatPackage(ClothingPackage package) {
	//		foreach (ClothingItem item in package.items) {
	//			ClothingItem clothingItem = (ClothingItem)item;
	//			if (!clothingItem.HasValidSpriteName()) Debug.LogError("item doesn't have a valid sprite! fix this or expect errors.");
	//			SetAttachment(clothingItem);
	//		}
	//	}
	//
	//	private void RemoveHatPackage(ClothingPackageType type) {
	//		ClothingPackage itemSet = GetEquippedPackage(type);
	//		foreach (CollectableItem item in itemSet.items) {
	//			ClothingItem clothingItem = (ClothingItem)item;
	//			RemoveAttachment(clothingItem.type);
	//		}
	//		ClothingPackageDataManager.RemoveEquippedItemSet(type);
	//	}
	//
	//	private void SetAttachment(ClothingItem clothingItem) {
	//		string slot = GetSlot(clothingItem.type);
	//		string path = GetSpritePath(clothingItem);
	//		SkeletonAnimation skeleton = GetSkeleton(clothingItem.skeleton);
	//
	//		skeleton.skeleton.SetAttachment(slot, path);
	//
	//		if (clothingItem.type == ClothingItemType.Hat) WhitTools.Invoke(OnHatEquipped);
	//		else if (clothingItem.type == ClothingItemType.ShoeBack) WhitTools.Invoke(OnShoeBackEquipped);
	//		else if (clothingItem.type == ClothingItemType.ShoeFront) WhitTools.Invoke(OnShoeFrontEquipped);
	//	}
	//
	//	private void RemoveAttachment(ClothingItemType type) {
	//		string slot = GetSlot(type);
	//		SkeletonAnimation skeleton = GetSkeleton(type);
	//
	//		skeleton.skeleton.SetAttachment(slot, null);
	//
	//		if (type == ClothingItemType.Hat) WhitTools.Invoke(OnHatUnequipped);
	//		else if (type == ClothingItemType.ShoeBack) WhitTools.Invoke(OnShoeBackUnequipped);
	//		else if (type == ClothingItemType.ShoeFront) WhitTools.Invoke(OnShoeFrontUnequipped);
	//	}
	//
	//	private SkeletonAnimation GetSkeleton(ClothingItemType type) {
	//		SkeletonAnimation skeleton = null;
	//		if (type == ClothingItemType.Hat) skeleton = GetSkeleton(ClothingSkeletonType.Top);
	//		else if (type == ClothingItemType.ShoeBack) skeleton = GetSkeleton(ClothingSkeletonType.Bottom); 
	//		else if (type == ClothingItemType.ShoeFront) skeleton = GetSkeleton(ClothingSkeletonType.Bottom); 
	//		else {
	//			Debug.LogError("invalid clothing item type: " + type.ToString());
	//			return null;
	//		}
	//		return skeleton;
	//	}
	//
	//	private SkeletonAnimation GetSkeleton(ClothingSkeletonType type) {
	//		SkeletonAnimation skeleton = null;
	//		if (type == ClothingSkeletonType.Top) skeleton = stickmanTop;
	//		else if (type == ClothingSkeletonType.Bottom) skeleton = stickmanBottom;
	//		else Debug.LogError("invalid skeleton type: " + type.ToString());
	//		return skeleton;
	//	}
	//
	//	private string GetSpritePath(ClothingItem clothingItem) {
	//		string path;
	//
	//		if (clothingItem.type == ClothingItemType.Hat) path = hatsPath + clothingItem.spriteName;
	//		else if (clothingItem.type == ClothingItemType.ShoeBack) path = backShoesPath + clothingItem.spriteName;
	//		else if (clothingItem.type == ClothingItemType.ShoeFront) path = frontShoesPath + clothingItem.spriteName;
	//		else {
	//			Debug.LogError("invalid clothing item type: " + clothingItem.type.ToString());
	//			path = "";
	//		}
	//
	//		return path;
	//	}
	//
	//	private string GetSlot(ClothingItemType type) {
	//		if (type == ClothingItemType.Hat) return hatSlot;
	//		else if (type == ClothingItemType.ShoeBack) return shoeBackSlot;
	//		else if (type == ClothingItemType.ShoeFront) return shoeFrontSlot;
	//		else {
	//			Debug.LogError("invalid clothing item type: " + type.ToString());
	//			return "";
	//		}
	//	}
}
