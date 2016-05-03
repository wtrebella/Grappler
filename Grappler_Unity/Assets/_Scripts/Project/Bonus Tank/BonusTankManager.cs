using UnityEngine;
using System.Collections;

public class BonusTankManager : MonoBehaviour {
	public static BonusTankManager instance;

	[SerializeField] private BonusTank bonusTank;

	private void Awake() {
		instance = this;
	}


}
