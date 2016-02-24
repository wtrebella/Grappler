using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine;
using System.Linq;
using UnityEngine.Events;

public class Clothing : MonoBehaviour {
	public UnityEvent OnHatEquipped;
	public UnityEvent OnShoeBackEquipped;
	public UnityEvent OnShoeFrontEquipped;

	public UnityEvent OnHatUnequipped;
	public UnityEvent OnShoeBackUnequipped;
	public UnityEvent OnShoeFrontUnequipped;

	[SerializeField] private tk2dSpriteCollectionData spriteCollection;
	[SerializeField] private SkeletonAnimation stickmanTop;
	[SerializeField] private SkeletonAnimation stickmanBottom;

	private ClothingItem[] hats;
	private ClothingItem[] shoesFront;
	private ClothingItem[] shoesBack;
	private Dictionary<ClothingItemType, ClothingItem> equippedClothingItems;
	private string hatSlot = "hat";
	private string hatsPath = "hats/";
	private string shoeBackSlot = "shoeBack";
	private string shoesBackPath = "shoes/shoesBack/";
	private string shoeFrontSlot = "shoeFront";
	private string shoesFrontPath = "shoes/shoesFront/";

	private void Awake() {
		equippedClothingItems = new Dictionary<ClothingItemType, ClothingItem>();
		LoadClothingItems();
	}

	private void Start() {

	}

	private void SetRandomItems() {
		SetAttachment(hats[Random.Range(0, hats.Length)]);
		SetAttachment(shoesFront[Random.Range(0, shoesFront.Length)]);
		SetAttachment(shoesBack[Random.Range(0, shoesBack.Length)]);
	}

	private void LoadClothingItems() {
		hats = Resources.LoadAll("Clothing Items/Hats", typeof(ClothingItem)).Cast<ClothingItem>().ToArray();
		shoesFront = Resources.LoadAll("Clothing Items/Shoes Front", typeof(ClothingItem)).Cast<ClothingItem>().ToArray();
		shoesBack = Resources.LoadAll("Clothing Items/Shoes Back", typeof(ClothingItem)).Cast<ClothingItem>().ToArray();
	}

	public ClothingItem[] GetHats() {
		return hats;
	}

	public ClothingItem[] GetShoesFront() {
		return shoesFront;
	}

	public ClothingItem[] GetShoesBack() {
		return shoesBack;
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

		if (type == ClothingItemType.Hat) WhitTools.Invoke(OnHatUnequipped);
		else if (type == ClothingItemType.ShoeBack) WhitTools.Invoke(OnShoeBackUnequipped);
		else if (type == ClothingItemType.ShoeFront) WhitTools.Invoke(OnShoeFrontUnequipped);
	}

	private void SetAttachment(ClothingItem clothingItem) {
		string slot = GetSlot(clothingItem.type);
		string path = GetPath(clothingItem);
		SkeletonAnimation skeleton = GetSkeleton(clothingItem.skeleton);

		equippedClothingItems.Add(clothingItem.type, clothingItem);
		skeleton.skeleton.SetAttachment(slot, path);

		if (clothingItem.type == ClothingItemType.Hat) WhitTools.Invoke(OnHatEquipped);
		else if (clothingItem.type == ClothingItemType.ShoeBack) WhitTools.Invoke(OnShoeBackEquipped);
		else if (clothingItem.type == ClothingItemType.ShoeFront) WhitTools.Invoke(OnShoeFrontEquipped);
	}

	private SkeletonAnimation GetSkeleton(ClothingItemType type) {
		SkeletonAnimation skeleton = null;
		if (type == ClothingItemType.Hat) skeleton = GetSkeleton(ClothingSkeletonType.Top);
		else if (type == ClothingItemType.ShoeBack || type == ClothingItemType.ShoeFront) skeleton = GetSkeleton(ClothingSkeletonType.Bottom); 
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

	private string GetPath(ClothingItem clothingItem) {
		string path;

		if (clothingItem.type == ClothingItemType.Hat) {
			path = hatsPath + clothingItem.spriteName;
		}
		else if (clothingItem.type == ClothingItemType.ShoeBack) {
			path = shoesBackPath + clothingItem.spriteName;
		}
		else if (clothingItem.type == ClothingItemType.ShoeFront) {
			path = shoesFrontPath + clothingItem.spriteName;
		}
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
