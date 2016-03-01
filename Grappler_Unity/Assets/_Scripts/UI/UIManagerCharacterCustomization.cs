using UnityEngine;
using System.Collections;

public class UIManagerCharacterCustomization : UIManager {
	private void Awake() {
		BaseAwake();
		GetPanelOfType<PanelCharacterCustomization>().gameObject.SetActive(true);
	}
}
