using UnityEngine;
using System.Collections;

public class ClothingManager : Singleton<ClothingManager> {
	public const string hatSpineSlotName = "hat";
	public const string backShoeSpineSlotName = "shoeBack";
	public const string frontShoeSpineSlotName = "shoeFront";

	[SerializeField] private WhitSpineSkeleton topSkeleton;
	[SerializeField] private WhitSpineSkeleton bottomSkeleton;

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
		InitializeCollections();
		InitializeEquipmentSlots();
		InitializeSpineSlots();
		InitializationComplete();
	}

	public IEnumerator WaitForInit() {
		while (!initializationComplete) {yield return null;}
	}

	private void InitializationComplete() {
		initializationComplete = true;
	}

	private void InitializeEquipmentSlots() {
		if (!EquipmentManager.DoesExist()) return;
		hatEquipmentSlot = EquipmentManager.instance.GetHatSlot();
		shoesEquipmentSlot = EquipmentManager.instance.GetShoesSlot();
	}

	private void InitializeCollections() {
		if (!CollectionManager.DoesExist()) return;
		hatCollection = CollectionManager.instance.GetCollection(CollectionType.Hats);
		shoesCollection = CollectionManager.instance.GetCollection(CollectionType.Shoes);
	}

	private void InitializeSpineSlots() {
		hatSpineSlot = topSkeleton.GetSlot(hatSpineSlotName);
		backShoeSpineSlot = bottomSkeleton.GetSlot(backShoeSpineSlotName);
		frontShoeSpineSlot = bottomSkeleton.GetSlot(frontShoeSpineSlotName);
	}

	public void WearSavedOrFirstItems() {
		WearSavedItems();

		if (!HatIsEquipped()) WearFirstHat();
		if (!ShoesAreEquipped()) WearFirstShoes();
	}

	public void WearSavedItems() {
		WearHat(hatEquipmentSlot.GetItem());
		WearShoes(shoesEquipmentSlot.GetItem());
	}

	public void WearFirstHat() {
		WearHat(hatCollection.GetFirstItem());
	}

	public void WearFirstShoes() {
		WearShoes(shoesCollection.GetFirstItem());
	}

	public void WearHat(CollectionItem item) {
		if (item == null) hatSpineSlot.RemoveSprite();
		else hatSpineSlot.SetSprite(item.GetFirstSprite());
	}

	public void WearShoes(CollectionItem item) {
		if (item == null) {
			backShoeSpineSlot.RemoveSprite();
			frontShoeSpineSlot.RemoveSprite();
		}
		else {
			backShoeSpineSlot.SetSprite(item.GetFirstSprite());
			frontShoeSpineSlot.SetSprite(item.GetSecondSprite());
		}
	}

	public bool HatIsEquipped() {
		return hatEquipmentSlot.IsEquipped();
	}

	public bool ShoesAreEquipped() {
		return shoesEquipmentSlot.IsEquipped();
	}
}
