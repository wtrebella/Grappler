using UnityEngine;
using System.Collections;

public class ClothingManager : Singleton<ClothingManager> {
	public const string hatSpineSlotName = "hat";
	public const string backShoeSpineSlotName = "shoeBack";
	public const string frontShoeSpineSlotName = "shoeFront";

	[SerializeField] private WhitSpineSkeleton bearSkeleton;

	private EquipmentSlot hatEquipmentSlot;
	private EquipmentSlot shoesEquipmentSlot;

	private WhitSpineSlot hatSpineSlot;
	private WhitSpineSlot backShoeSpineSlot;
	private WhitSpineSlot frontShoeSpineSlot;

	private Collection hatCollection;
	private Collection shoesCollection;

	private bool initializationComplete = false;

	//	public UnityEvent OnHatEquipped;
	//	public UnityEvent OnShoeBackEquipped;
	//	public UnityEvent OnShoeFrontEquipped;
	//
	//	public UnityEvent OnHatUnequipped;
	//	public UnityEvent OnShoeBackUnequipped;
	//	public UnityEvent OnShoeFrontUnequipped;

	private void Start() {
		if (!GameStateManager.DoesExist()) return;

		InitializeCollections();
		InitializeEquipmentSlots();
		InitializeSpineSlots();
		InitializationComplete();

		WearSavedOrFirstItems();
		//		WearHat(CollectionManager.instance.GetItem(CollectionType.Hats, "Baseball Cap"));
		//		WearShoes(CollectionManager.instance.GetItem(CollectionType.Shoes, "Converse"));
	}

	public IEnumerator WaitForInit() {
		while (!initializationComplete) {yield return null;}
	}

	private void InitializationComplete() {
		initializationComplete = true;
	}

	private void InitializeEquipmentSlots() {
		hatEquipmentSlot = EquipmentManager.instance.GetHatSlot();
		shoesEquipmentSlot = EquipmentManager.instance.GetShoesSlot();
	}

	private void InitializeCollections() {
		hatCollection = CollectionManager.instance.GetCollection(CollectionType.Hats);
		shoesCollection = CollectionManager.instance.GetCollection(CollectionType.Shoes);
	}

	private void InitializeSpineSlots() {
		hatSpineSlot = bearSkeleton.GetSlot(hatSpineSlotName);
	}

	public void WearSavedOrFirstItems() {
		WearSavedItems();

		if (!HatIsEquipped()) WearFirstHat();
		if (!ShoesAreEquipped()) WearFirstShoes();
	}

	public void WearSavedItems() {
		CollectionItem savedHat = hatEquipmentSlot.GetItem();
		CollectionItem savedShoes = shoesEquipmentSlot.GetItem();
		if (savedHat) WearHat(savedHat);
		if (savedShoes) WearShoes(savedShoes);
	}

	public void WearFirstHat() {
		WearHat(hatCollection.GetFirstItem());
	}

	public void WearFirstShoes() {
		WearShoes(shoesCollection.GetFirstItem());
	}

	public void WearItem(CollectionItem item) {
		if (item.type == CollectionItemType.Hat) WearHat(item);
		else if (item.type == CollectionItemType.Shoes) WearShoes(item);
	}

	public void RemoveHat() {
		hatSpineSlot.RemoveSprite();
		hatEquipmentSlot.RemoveItem();
	}

	public void RemoveShoes() {
		backShoeSpineSlot.RemoveSprite();
		frontShoeSpineSlot.RemoveSprite();
		shoesEquipmentSlot.RemoveItem();
	}

	public void WearHat(CollectionItem item) {
		hatSpineSlot.SetSprite((SpineCollectionItemSprite)item.GetFirstSprite());
		hatEquipmentSlot.EquipItem(item);
	}

	public void WearShoes(CollectionItem item) {
		backShoeSpineSlot.SetSprite((SpineCollectionItemSprite)item.GetFirstSprite());
		frontShoeSpineSlot.SetSprite((SpineCollectionItemSprite)item.GetSecondSprite());
		shoesEquipmentSlot.EquipItem(item);
	}

	public bool HatIsEquipped() {
		return hatEquipmentSlot.IsEquipped();
	}

	public bool ShoesAreEquipped() {
		return shoesEquipmentSlot.IsEquipped();
	}
}
