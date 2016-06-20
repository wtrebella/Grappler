using UnityEngine;
using System.Collections;

public class UIManagerGift : UIManager {
	private void Start() {
		GetPanelOfType<PanelGift>().gameObject.SetActive(true);
	}
}
