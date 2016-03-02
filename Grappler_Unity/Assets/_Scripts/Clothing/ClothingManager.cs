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

	private ClothingItemSet[] hats;
	private ClothingItemSet[] shoes;

	private string itemSetPathRoot = "clothingItems/";
	private string itemSetPathHats = "hats/hatsItemSets/";
	private string itemSetPathShoes = "shoes/shoesItemSets/";

	private string spritePathHats = "hats/";
	private string spritePathShoesBack = "shoes/shoesBack/";
	private string spritePathShoesFront = "shoes/shoesFront/";

	private string hatSlot = "hat";
	private string shoeBackSlot = "shoeBack";
	private string shoeFrontSlot = "shoeFront";

	private void Awake() {
		LoadClothingItemSets();
	}

	private void Start() {
		EquipSavedItemSets();
	}

	private void SetRandomClothingItemSets() {
		SetClothingItemSet(hats[Random.Range(0, hats.Length)]);
		SetClothingItemSet(shoes[Random.Range(0, shoes.Length)]);
	}

	private void LoadClothingItemSets() {
		hats = Resources.LoadAll(itemSetPathRoot + itemSetPathHats, typeof(ClothingItemSet)).Cast<ClothingItemSet>().ToArray();
		shoes = Resources.LoadAll(itemSetPathRoot + itemSetPathShoes, typeof(ClothingItemSet)).Cast<ClothingItemSet>().ToArray();
	}

	public ClothingItemSet[] GetHats() {
		return hats;
	}

	public ClothingItemSet[] GetShoes() {
		return shoes;
	}

	public ClothingItemSet[] GetClothingItemSets(ClothingItemSetType type) {
		if (type == ClothingItemSetType.Hat) return GetHats();
		else if (type == ClothingItemSetType.Shoes) return GetShoes();
		else {
			Debug.LogError("invalid clothing item set type: " + type.ToString());
			return null;
		}
	}

	public void EquipNextHat() {
		EquipNext(ClothingItemSetType.Hat);
	}

	public void EquipNextShoes() {
		EquipNext(ClothingItemSetType.Shoes);
	}

	public void EquipNext(ClothingItemSetType type) {
		ClothingItemSet equippedItemSet = GetEquippedItemSet(type);
		UnequipItemSet(type);

		int currentItemIndex = -1;
		ClothingItemSet[] sets = GetClothingItemSets(type);

		if (equippedItemSet != null) {
			for (int i = 0; i < sets.Length; i++) {
				ClothingItemSet arrayItemSet = sets[i];
				if (equippedItemSet == arrayItemSet) {
					currentItemIndex = i;
				}
			}
		}

		IntRange wrapRange = new IntRange(-1, sets.Length);
		int newItemIndex = WhitTools.IncrementWithWrap(currentItemIndex, wrapRange);
		if (newItemIndex >= 0 && newItemIndex < sets.Length) {
			ClothingItemSet nextItem = sets[newItemIndex];
			EquipItemSet(nextItem);
		}
	}

	public void EquipItemSet(ClothingItemSet clothingItemSet) {
		SetClothingItemSet(clothingItemSet);
	}

	public void UnequipItemSet(ClothingItemSetType type) {
		if (ItemSetIsEquipped(type)) RemoveClothingItemSet(type);
	}

	public bool ItemSetIsEquipped(ClothingItemSetType type) {
		return EquippedClothing.instance.equippedSets.ContainsKey(type);
	}

	public ClothingItemSet GetEquippedItemSet(ClothingItemSetType type) {
		if (!EquippedClothing.instance.equippedSets.ContainsKey(type)) return null;

		return EquippedClothing.instance.equippedSets[type];
	}

	private void EquipSavedItemSets() {
		var equippedItemSets = EquippedClothing.instance.GetEquippedItemSets();
		foreach (ClothingItemSet itemSet in equippedItemSets) EquipItemSet(itemSet);
	}

	private void SetClothingItemSet(ClothingItemSet clothingItemSet) {
		foreach (ClothingItem item in clothingItemSet.items) {
			if (!item.HasValidSpriteName()) Debug.LogError("item doesn't have a valid sprite! fix this or expect errors.");
			SetAttachment(item);
		}
		EquippedClothing.instance.equippedSets.Add(clothingItemSet.type, clothingItemSet);
	}

	private void RemoveClothingItemSet(ClothingItemSetType type) {
		ClothingItemSet itemSet = GetEquippedItemSet(type);
		foreach (ClothingItem item in itemSet.items) RemoveAttachment(item.type);
		if (EquippedClothing.instance.equippedSets.ContainsKey(type)) EquippedClothing.instance.equippedSets.Remove(type);
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
}
