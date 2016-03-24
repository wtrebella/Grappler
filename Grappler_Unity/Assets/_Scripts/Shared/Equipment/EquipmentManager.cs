using UnityEngine;
using System.Collections;

public class EquipmentManager : MonoBehaviour {
	public static EquipmentManager instance;

	[SerializeField] private EquipmentSlot hatSlot;
	[SerializeField] private EquipmentSlot shoesSlot;
	[SerializeField] private EquipmentSlot powerUpSlot1;
	[SerializeField] private EquipmentSlot powerUpSlot2;

	private void Awake() {
		InitializeSingleton();
	}

	private void InitializeSingleton() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(this.gameObject);
		}
	}
}
